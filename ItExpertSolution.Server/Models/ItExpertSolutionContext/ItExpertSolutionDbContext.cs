using ItExpertSolution.Server.Models.Model.Solutions.Domain;
using Microsoft.EntityFrameworkCore;

namespace ItExpertSolution.Server.Models.ItExpertSolution
{
    public class ItExpertSolutionDbContext : DbContext
    {
        public ItExpertSolutionDbContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Solution>().HasKey(k => k.Id);
            modelBuilder.Entity<Solution>().HasIndex(i => i.Code);
            modelBuilder.Entity<Solution>().HasIndex(i => i.Ordering);
        }

        public DbSet<Solution> Solutions { get; set; }
    }
}
