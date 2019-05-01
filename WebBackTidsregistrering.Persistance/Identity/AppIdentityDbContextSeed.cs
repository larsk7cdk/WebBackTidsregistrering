using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace WebBackTidsregistrering.Persistance.Identity
{
    public class AppIdentityDbContextSeed
    {
        public static async Task SeedAsync(UserManager<IdentityUser> userManager)
        {
            var user = await userManager.FindByEmailAsync("lars@k7c.dk");
            if (user != null) return;

            var defaultUser = new IdentityUser
            {
                Email = "lars@k7c.dk",
                UserName = "lars@k7c.dk"
            };

            await userManager.CreateAsync(defaultUser, "P@ssword1");
        }
    }
}