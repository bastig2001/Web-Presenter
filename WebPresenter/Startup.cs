using System;
using System.Data.Common;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WebPresenter.Data;
using WebPresenter.Hubs;
using WebPresenter.Models;
using WebPresenter.Services;

namespace WebPresenter {
    public class Startup {
        public Startup(IConfiguration configuration) {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services) {
            services.AddControllersWithViews();
            // In production, the Angular files will be served from this directory
            services.AddSpaStaticFiles(configuration => { configuration.RootPath = "ClientApp/dist"; });

            services.AddSignalR(options => options.EnableDetailedErrors = true);

            var dbConnectionSettings = new DbConnectionSettings();
            Configuration.GetSection("DB").Bind(dbConnectionSettings);
            
            services.AddDbContext<WebPresenterContext>(options => 
                options.UseNpgsql(dbConnectionSettings.GetConnectionString())
            );

            services.AddSingleton<StorageService<Presentation>>();
            services.AddSingleton<StorageService<Viewer>>();
            
            services.AddTransient<PresentationDataService>();
            services.AddTransient<PresentationsService>();
            services.AddTransient<ConnectionManager>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
            InitiateDatabase(app);
            
            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
            }
            else {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            // app.UseHttpsRedirection();
            app.UseStaticFiles();
            if (!env.IsDevelopment()) {
                app.UseSpaStaticFiles();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints => {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");
                endpoints.MapHub<PresentationsHub>("hubs/presentations");
            });

            app.UseSpa(spa => {
                // To learn more about options for serving an Angular SPA from ASP.NET Core,
                // see https://go.microsoft.com/fwlink/?linkid=864501

                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment()) {
                    spa.UseAngularCliServer(npmScript: "start");
                }
            });
        }

        private static void InitiateDatabase(IApplicationBuilder app) {
            using var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope();
            using var dbContext = serviceScope.ServiceProvider.GetRequiredService<WebPresenterContext>();
            
            dbContext.Database.EnsureCreated();
                    
            // Inserts for testing purposes
            if (dbContext.Users.Find("anyone") == null) {
                dbContext.Users.Add(new User("anyone"));
            }

            dbContext.SaveChanges();
        }
    }
}