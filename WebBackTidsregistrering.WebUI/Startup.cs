using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using WebBackTidsregistrering.Application.Interfaces;
using WebBackTidsregistrering.Application.Services;
using WebBackTidsregistrering.Domain.Entities;
using WebBackTidsregistrering.Persistance.Data;
using WebBackTidsregistrering.Persistance.Identity;

namespace WebBackTidsregistrering.WebUI
{
    public class Startup
    {
        private readonly List<CultureInfo> _supportedCultures = new List<CultureInfo>
        {
            new CultureInfo("da-DK"),
            new CultureInfo("en-US")
        };

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });


            services.AddDbContext<AppIdentityDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("Default")));

            services.AddIdentity<IdentityUser, IdentityRole>()
                .AddDefaultUI(UIFramework.Bootstrap4)
                .AddEntityFrameworkStores<AppIdentityDbContext>()
                .AddDefaultTokenProviders();


            services.AddDbContext<AppDataDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("Default")));

            services.AddAuthentication()
                .AddCookie(options =>
                {
                    options.Cookie.Name = "WebBackTidsregistreringCookie";
                    options.AccessDeniedPath = "/Account/AccessDenied";
                    options.ExpireTimeSpan = new TimeSpan(0, 20, 0);
                });

            services.Configure<RequestLocalizationOptions>(options =>
            {
                options.DefaultRequestCulture = new RequestCulture("da-DK");
                options.SupportedCultures = _supportedCultures;
                options.SupportedUICultures = _supportedCultures;
            });

            services.AddMvc(options =>
                {
                    options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
                    options.Filters.Add(new RequireHttpsAttribute());
                })
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);


            services.AddTransient<IRepository<Registration>, Repository<Registration>>();
            services.AddTransient<IRegistrationService, RegistrationService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            var logger = loggerFactory.CreateLogger("Logs");
            logger.LogInformation($"Tidsregistrering Web UI startet {DateTime.Now:dd-MM-yyyy HH:mm}");

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseCookiePolicy();
            app.UseRequestLocalization();

            app.UseMvcWithDefaultRoute();
        }
    }
}