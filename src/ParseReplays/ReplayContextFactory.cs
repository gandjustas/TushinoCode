using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tushino;

namespace ParseTsgReplays
{
    public class ReplaysContextFactory : IDesignTimeDbContextFactory<ReplaysContext>
    {
        public ReplaysContext CreateDbContext(string[] args)
        {
            IConfiguration config = new ConfigurationBuilder()
                            .AddJsonFile("appsettings.json")
                            .AddEnvironmentVariables()
                            .AddCommandLine(args)
                            .Build();
            var optionsBuilder = new DbContextOptionsBuilder<ReplaysContext>();
            optionsBuilder.UseNpgsql(config.GetConnectionString("postgres"));

            return new ReplaysContext(optionsBuilder.Options);
        }
    }
}
