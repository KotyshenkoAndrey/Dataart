using Microsoft.EntityFrameworkCore;
using DataArt.Entities;

namespace DataArt.Database
{
    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions options) : base(options) { }

        public DbSet<Incident> IncidentDb { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Incident>().ToTable("task");
            modelBuilder.Entity<Incident>().Property(x => x.Id).IsRequired();
            modelBuilder.Entity<Incident>().Property(x => x.Type).IsRequired();
            modelBuilder.Entity<Incident>().HasIndex(x => x.Time);
        }

    }
}
