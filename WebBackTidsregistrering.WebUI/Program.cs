using System;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;
using WebBackTidsregistrering.Persistance.Data;
using WebBackTidsregistrering.Persistance.Identity;

namespace WebBackTidsregistrering.WebUI
{
    public class Program
    {
        public static void Main(string[] args)
        {
      

            Log.Logger = new LoggerConfiguration()
                .WriteTo.RollingFile("Logs/web-ui.log")
                .CreateLogger();

            var host = CreateWebHostBuilder(args)
                .Build();
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                try
                {
                    var userManager = services.GetRequiredService<UserManager<IdentityUser>>();
                    AppIdentityDbContextSeed.SeedAsync(userManager).Wait();

                    var context = scope.ServiceProvider.GetService<AppDataDbContext>();
                    AppDataDbContextSeed.Seed(context);
                }

                catch (Exception ex)
                {
                    Log.Error(ex, "Fejl ved opstart af Tidsregistrering Web UI");
                }
            }

            host.Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args)
        {
            return WebHost.CreateDefaultBuilder(args)
                .UseSerilog()
                .UseStartup<Startup>();
        }
    }
}