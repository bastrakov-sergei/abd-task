using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using Hangfire;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebApp.Data;
using WebApp.Hangfire;
using WebApp.Models;
using WebApp.Services.Account;
using WebApp.Services.DataFiles;
using WebApp.Services.DataFiles.Handlers;
using WebApp.Services.TradePoints;

namespace WebApp
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<AccountDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("AccountsDb")));
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("ApplicationDb")));
            services.AddDbContext<HangfireDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("HangfireDb")));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<AccountDbContext>()
                .AddDefaultTokenProviders();

            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 6;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
            });

            services.AddTransient<IAccountService, AccountService>();
            services.AddTransient<ITradePointsService, TradePointsService>();
            services.AddTransient<IDataFilesStore, DataFilesStore>();
            services.AddTransient<HashAlgorithm>(_ => SHA256.Create());
            services.AddTransient<IDataFileProcessor, DataFileProcessor>();

            services.AddTransient<JsonDataFileHandler>();
            services.AddTransient<XmlDataFileHandler>();
            services.AddTransient<TextDataFileHandler>();

            services.AddTransient<IEnumerable<IDataFileHandler>>(provider => new[]
            {
                (IDataFileHandler) provider.GetRequiredService<JsonDataFileHandler>(),
                (IDataFileHandler) provider.GetRequiredService<XmlDataFileHandler>(),
                (IDataFileHandler) provider.GetRequiredService<TextDataFileHandler>()

            });

            services.AddHangfire(config => config.UseSqlServerStorage(Configuration.GetConnectionString("HangfireDb")));

            services.AddMvc(config =>
            {
                config.ModelBinderProviders.Insert(0, new InvariantDoubleModelBinderProvider());
            });
        }

        public void Configure(
            IApplicationBuilder app,
            IHostingEnvironment env,
            IServiceProvider serviceProvider)
        {

            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();
            app.UseAuthentication();

            GlobalConfiguration.Configuration.UseActivator(new ContainerJobActivator(serviceProvider));
            app.UseHangfireServer();
            app.UseHangfireDashboard("/jobs");

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
