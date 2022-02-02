using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tushino
{
    public class ReplaysContext : DbContext
    {
        public DbSet<Replay> Replays { get; set; }
        public DbSet<Unit> Units { get; set; }
        public DbSet<EnterExit> EnterExit { get; set; }
        public DbSet<Kill> Kills { get; set; }
        public DbSet<Hit> Hits { get; set; }
        public DbSet<Medical> Medicals { get; set; }
        public DbSet<Goal> Goals { get; set; }


        public ReplaysContext(DbContextOptions<ReplaysContext> options): base(options)
        {
            //AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        }

    }
}
