using Business.Entities;
using Business.Service;
using ERP.Helpers;
using ERP.Permission;
using GridMvc;
using Kinfo.JsonStore.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace ERP
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
            // Repos etc
            DependencyContainer.RegisterServices(services);
            services.AddGridMvc();

            services.AddMvc(o => o.EnableEndpointRouting = false);

            services.AddSingleton<IAuthorizationPolicyProvider, PermissionPolicyProvider>();
            services.AddScoped<IAuthorizationHandler, PermissionAuthorizationHandler>();
            services.AddIdentity<UserMasterMetadata, RoleMasterMetadata>()
                .AddDefaultTokenProviders();
            services.Configure<IdentityOptions>(options =>
            {
                // Password settings.
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = true;
                options.Password.RequiredLength = 8;

                // Lockout settings.
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;

                // User settings.
                options.User.AllowedUserNameCharacters =
                "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                options.User.RequireUniqueEmail = true;
            });
            services.AddScoped<IUserClaimsPrincipalFactory<UserMasterMetadata>, CustomClaimsPrincipal>();
            services.AddMvc()
        .AddSessionStateTempDataProvider();
            services.AddSession();
            //services.AddSingleton<IMvcControllerDiscovery, MvcControllerDiscovery>();
            //services.ConfigureApplicationCookie(options =>
            //{
            //    // Cookie settings
            //    options.Cookie.HttpOnly = true;
            //    options.ExpireTimeSpan = TimeSpan.FromMinutes(90);
            //    options.LoginPath = "/";
            //    options.AccessDeniedPath = "/404";
            //    options.SlidingExpiration = true;
            //    // options.Events = new MyCookieAuthenticationEvents();

            //});


            services.AddControllersWithViews();
            services.AddRazorPages();
            //services.AddJsonStore();

            services.AddDynamicAuthorization(options => options.DefaultAdminUser = "mo.esmp@gmail.com")
                .AddJsonStore();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                //app.UseDeveloperExceptionPage();
                //app.UseDatabaseErrorPage();
                app.UseExceptionHandler("/Home/Error");
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.Use(async (context, next) =>
            {
                await next();
                if (context.Response.StatusCode == 404)
                {
                    context.Request.Path = "/Home/Error";
                    await next();
                }
            });

            app.UseHttpsRedirection();
            //app.UseStaticFiles();
            app.UseStaticFiles(new StaticFileOptions()
            {
                OnPrepareResponse = context =>
                {
                    context.Context.Response.Headers.Add("Cache-Control", "no-cache, no-store");
                    context.Context.Response.Headers.Add("Expires", "-1");
                }
            });
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseSession();
            app.UseEndpoints(endpoints =>
            {
                //endpoints.MapAreaControllerRoute(
                //name: "SuperAdmin",
                //areaName: "SuperAdmin",
                //pattern: "SuperAdmin/{controller=Home}/{action=Index}/{id?}");

                //endpoints.MapAreaControllerRoute(
                //name: "Admin",
                //areaName: "Admin",
                //pattern: "Admin/{controller=Home}/{action=Index}/{id?}");

                /*endpoints.MapAreaControllerRoute(
                name: "Marketing",
                areaName: "Marketing",
                pattern: "Marketing/{controller=Home}/{action=Index}/{id?}");*/

                endpoints.MapControllerRoute(
                    name: "default-area",
                    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}
