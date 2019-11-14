using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using MultiSPA.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.IO;
using Microsoft.Extensions.FileProviders;
using MultiSPA.Data.Entities;

namespace MultiSPA
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlite(
                    Configuration.GetConnectionString("DefaultConnection")));

            services.AddDefaultIdentity<ApplicationUser>()
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultUI();

            // AddNewtonsoftJson() let's us ignore circular references in json data. The nuget package Microsoft.AspNetCore.Mvc.NewtonsoftJson 3.0.0 needs to be installed
            // Call opts.SerializerSettings.ContractResolver = null; to prevent json from being converted to camelCase on serialization
            services.AddControllersWithViews()
                .AddNewtonsoftJson(opts => {
                    opts.SerializerSettings.ContractResolver = null;
                    opts.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                });

            services.AddRazorPages();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });

            // This code tells the .NET core runtime to map the app to /apps/app1 path, to proxy to port 4201 in development 
            // and to expect the compiled files to be available in wwwroot/apps/app1 in non-development environments.
            app.Map("/apps/app1", builder => {
                builder.UseSpa(spa =>
                {
                    if (env.IsDevelopment())
                    {
                        spa.UseProxyToSpaDevelopmentServer($"http://localhost:4201/");
                    }
                    else
                    {
                        var staticPath = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot/Apps/dist/app1");
                        var fileOptions = new StaticFileOptions { FileProvider = new PhysicalFileProvider(staticPath) };
                        builder.UseSpaStaticFiles(options: fileOptions);

                        spa.Options.DefaultPageStaticFileOptions = fileOptions;
                    }
                });
            });

            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                context.Database.Migrate();
            }
        }
    }
}
