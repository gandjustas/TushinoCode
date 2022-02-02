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

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Filename=replays.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Unit>()
                .HasKey("ReplayId", "Id");

            base.OnModelCreating(modelBuilder);
        }
    }
}
