using Microsoft.EntityFrameworkCore;
using DataArt.Entities;

namespace DataArt.Database
{
    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions options) : base(options) { }

        public DbSet<Incident> IncidentDb { get; set; }
        public DbSet<Event> EventDb { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Incident>().ToTable("incident");
            modelBuilder.Entity<Incident>().Property(x => x.Id).IsRequired();
            modelBuilder.Entity<Incident>().Property(x => x.Type).IsRequired();
            modelBuilder.Entity<Incident>().HasIndex(x => x.Time);

            modelBuilder.Entity<Event>().ToTable("event");
            modelBuilder.Entity<Event>().Property(x => x.Id).IsRequired();
            modelBuilder.Entity<Event>().Property(x => x.Type).IsRequired();
            modelBuilder.Entity<Event>().HasIndex(x => x.Time);
        }

    }
}
