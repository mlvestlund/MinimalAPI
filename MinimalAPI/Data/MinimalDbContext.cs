using Microsoft.EntityFrameworkCore;
using MinimalAPI.Models;

namespace MinimalAPI.Data
{
    public class MinimalDbContext : DbContext
    {
        public DbSet<Person> Persons { get; set; }
        public DbSet<Interest> Interests { get; set; }
        public DbSet<PersonInterest> PersonInterests { get; set; }
        public DbSet<Link> Links { get; set; }

        public MinimalDbContext(DbContextOptions<MinimalDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PersonInterest>()
                .HasKey(pi => new { pi.PersonID, pi.InterestID });

            base.OnModelCreating(modelBuilder);
        }
    }
}
