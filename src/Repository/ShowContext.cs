using DomainModels;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    public class ShowContext : DbContext
    {
        public DbSet<Show> Shows { get; set; }
        public DbSet<Actor> Actors { get; set; }
        public DbSet<ShowActor> ShowActors { get; set; }
        public DbSet<ShowRate> ShowRates { get; set; }
        public DbSet<ShowType> ShowTypes { get; set; }

        public ShowContext(DbContextOptions<ShowContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ShowActor>().HasKey(s => new { s.ShowId, s.ActorId });
            modelBuilder.Entity<ShowRate>().HasKey(s => new { s.ShowId, s.UserId });
        }
    }
}
