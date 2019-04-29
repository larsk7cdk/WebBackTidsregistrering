using Microsoft.EntityFrameworkCore;
using WebBackTidsregistrering.Persistance.Infrastructure;

namespace WebBackTidsregistrering.Persistance.Identity
{
    public class AppIdentityDbContextFactory : DesignTimeDbContextFactoryBase<AppIdentityDbContext>
    {
        protected override AppIdentityDbContext CreateNewInstance(DbContextOptions<AppIdentityDbContext> options)
        {
            return new AppIdentityDbContext(options);
        }
    }
}