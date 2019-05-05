using Microsoft.EntityFrameworkCore;
using WebBackTidsregistrering.Persistance.Infrastructure;

namespace WebBackTidsregistrering.Persistance.Data
{
    public class AppDataDbContextFactory : DesignTimeDbContextFactoryBase<AppDataDbContext>
    {
        protected override AppDataDbContext CreateNewInstance(DbContextOptions<AppDataDbContext> options)
        {
            return new AppDataDbContext(options);
        }
    }
}