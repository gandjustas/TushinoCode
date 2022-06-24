using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using System.Threading;
using Microsoft.EntityFrameworkCore;
using PboTools;
using SharpCompress.Archives.SevenZip;
using System.Threading.Channels;
using Microsoft.Extensions.Configuration;

namespace Tushino
{
    public record ReplayKey(string server, DateTime timestamp);
    public class Program
    {
        static int counterParsed = 0;
        static int counterProcessed = 0;
        static Channel<Tuple<string, Exception>> exceptions = Channel.CreateUnbounded<Tuple<string, Exception>>(new UnboundedChannelOptions { SingleReader = true });
        static Channel<Replay> queue = Channel.CreateUnbounded<Replay>(new UnboundedChannelOptions { SingleReader = true });
        static HashSet<ReplayKey> ExistingRecords = new HashSet<ReplayKey>();
        public async static Task Main(string[] args)
        {
            IConfiguration config = new ConfigurationBuilder()
                                        .AddJsonFile("appsettings.json")
                                        .AddEnvironmentVariables()
                                        .AddCommandLine(args)
                                        .Build();

            var optionsBuilder = new DbContextOptionsBuilder<ReplaysContext>();
            optionsBuilder.UseNpgsql(config.GetConnectionString("postgres"));
            var contextOptions = optionsBuilder.Options;

            if (args.Length == 0)
            {
                Console.WriteLine("Usage: ParseTsgReplays.exe path-to-replays");
                return;
            }


            using (var db = new ReplaysContext(contextOptions))
            {
                db.Database.Migrate();
                var allReplays = db.Replays.AsQueryable();
                ExistingRecords = allReplays.Select(r => new ReplayKey(r.Server, r.Timestamp)).ToHashSet();
            };

            var dir = args[0];

            if (Path.HasExtension(dir))
            {
                var replay = Path.GetExtension(dir) switch
                {
                    ".pbo" => ParseSiglePbo(dir),
                    ".7z" => ParseSingleArchive(dir),
                    _ => throw new InvalidOperationException("Unknown file extension")
                };
                using var db = new ReplaysContext(contextOptions);
                if (ExistingRecords.TryGetValue(new ReplayKey(replay.Server, replay.Timestamp), out var key))
                {
                    if (config.GetValue<bool>("Overwrite", false))
                    {
                        var toRemove = await db.Replays.Where(r => r.Server == key.server && r.Timestamp == key.timestamp).ToListAsync();
                        db.Replays.RemoveRange(toRemove);
                    }
                    else
                    {
                        throw new InvalidOperationException($"Replay from {key.server} {key.timestamp} already exists");
                    }
                }
                db.Replays.AddRange(replay);
                await db.SaveChangesAsync();

            }
            else
            {
                var task = Task.Run(async () =>
                {
                    var reader = queue.Reader;
                    List<Replay> toAdd = new();
                    while (await reader.WaitToReadAsync())
                    {
                        while (reader.TryRead(out Replay r))
                        {
                            if (ExistingRecords.Add(new(r.Server, r.Timestamp)))
                            {
                                toAdd.Add(r);
                            }
                        }
                        if (toAdd.Any())
                        {
                            using (var db = new ReplaysContext(contextOptions))
                            {
                                db.Replays.AddRange(toAdd);
                                await db.SaveChangesAsync();
                                toAdd.Clear();
                            }
                        }
                    }
                });


                var task2 = Task.Run(async () =>
                {
                    await foreach (var t in exceptions.Reader.ReadAllAsync())
                    {
                        Console.WriteLine(t.Item1);
                        Console.WriteLine(t.Item2.ToString());
                        Console.WriteLine();
                    }
                });
#if DEBUG
                foreach (var f in Directory.EnumerateFiles(dir, "*.pbo", SearchOption.AllDirectories))
                {
                    ParsePbo(f);
                };
                foreach (var f in Directory.EnumerateFiles(dir, "*.7z", SearchOption.AllDirectories))
                {
                    ParseArchive(f);
                };
#else
                Parallel.ForEach(Directory.EnumerateFiles(dir, "*.pbo", SearchOption.AllDirectories), ParsePbo);
                Parallel.ForEach(Directory.EnumerateFiles(dir, "*.7z", SearchOption.AllDirectories), ParseArchive);
#endif
                Console.WriteLine("Processed {0} parsed {1}", counterProcessed, counterParsed);

                queue.Writer.TryComplete();
                exceptions.Writer.TryComplete();

                await task;
                await task2;
            }



            //ClearDuplicates();
        }

        static Replay ParseSiglePbo(string pbo)
        {
            var replayName = Path.GetFileNameWithoutExtension(pbo);
            using var file = File.OpenRead(pbo);
            return ParseSingleReplay(replayName, file);
        }
        static Replay ParseSingleReplay(string replayName, Stream file)
        {
            var stream = PboFile.FromStream(file).OpenFile("log.txt");
            if (stream == null) return null;
            using var input = new StreamReader(stream);
            var p = new ReplayProcessor(input);
            var replay = p.ProcessReplay();
            replay.Server = replayName.Substring(0, 2);
            return replay;
        }
        static Replay ParseSingleArchive(string archive)
        {
            using var arch = SevenZipArchive.Open(archive);
            foreach (var ent in arch.Entries)
            {
                if (Path.GetExtension(ent.Key) == ".pbo")
                {
                    var replayName = Path.GetFileNameWithoutExtension(ent.Key);
                    using var file = ent.OpenEntryStream();
                    return ParseSingleReplay(replayName, file);
                }
            }
            throw new InvalidOperationException("Replay not found in archive");
        }

        static void ParsePbo(string pbo)
        {
            var replayName = Path.GetFileNameWithoutExtension(pbo);
            using (var file = File.OpenRead(pbo))
            {
                ParseReplayLog(replayName, file);
            }
            if (counterProcessed % 100 == 0) Console.WriteLine("Processed {0} parsed {1}", counterProcessed, counterParsed);
        }

        static void ParseArchive(string archive)
        {
            try
            {
                using (var arch = SevenZipArchive.Open(archive))
                {
                    foreach (var ent in arch.Entries)
                    {
                        var replayName = Path.GetFileNameWithoutExtension(ent.Key);
                        using (var file = ent.OpenEntryStream())
                        {
                            ParseReplayLog(replayName, file);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                exceptions.Writer.TryWrite(Tuple.Create(archive, e));
            }
            if (counterProcessed % 100 == 0) Console.WriteLine("Processed {0} parsed {1}", counterProcessed, counterParsed);
        }



        static void ParseReplayLog(string replayName, Stream file)
        {
            var key = new ReplayKey(
                  replayName.Substring(0, 2),
                  DateTime.ParseExact(replayName.Substring(3, 19), "yyyy-MM-dd-HH-mm-ss", CultureInfo.InvariantCulture)
                );
            if (ExistingRecords.Contains(key)) return;

            var stream = PboFile.FromStream(file).OpenFile("log.txt");

            if (stream != null)
            {
                using (var input = new StreamReader(stream))
                {
                    var p = new ReplayProcessor(input);
                    Replay replay;
                    try
                    {
                        replay = p.ProcessReplay();
                        if (replay != null)
                        {
                            Interlocked.Increment(ref counterParsed);
                        }
                    }
                    catch (ParseException e)
                    {
                        exceptions.Writer.TryWrite(Tuple.Create(replayName, (Exception)e));
                        replay = p.GetResult();
                    }
                    Interlocked.Increment(ref counterProcessed);
                    if (replay != null)
                    {
                        replay.Server = replayName.Substring(0, 2);
                        queue.Writer.TryWrite(replay);
                    }
                }
            }
        }
    }
}
