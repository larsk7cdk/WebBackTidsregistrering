using Microsoft.EntityFrameworkCore;
using WebBackTidsregistrering.Domain.Entities;

namespace WebBackTidsregistrering.Persistance.Data
{
    public class AppDataDbContext : DbContext
    {
        public AppDataDbContext(DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<Registration> Registrations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDataDbContext).Assembly);
        }
    }
}