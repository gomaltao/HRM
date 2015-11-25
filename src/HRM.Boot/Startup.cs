﻿using System;
    using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
//using Microsoft.AspNet.Authentication.Facebook;
//using Microsoft.AspNet.Authentication.Google;
//using Microsoft.AspNet.Authentication.MicrosoftAccount;
//using Microsoft.AspNet.Authentication.Twitter;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Diagnostics.Entity;
using Microsoft.AspNet.Hosting;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;
using Microsoft.Data.Entity;
//using Microsoft.Dnx.Runtime;
using Microsoft.Extensions.PlatformAbstractions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using HRM.Infrastructure.Services;
using HRM.Infrastructure.Identity;
using HRM.Infrastructure.Data;
using HRM.Infrastructure.Repository;
using HRM.Domain.Repository;
using HRM.Domain.Model;

namespace HRM.Boot 
{
    public class Startup
    {
        public Startup(IHostingEnvironment env, IApplicationEnvironment appEnv)
        {
            // Setup configuration sources.

            var builder = new ConfigurationBuilder()
                .SetBasePath(appEnv.ApplicationBasePath)
                .AddJsonFile("appsettings.json")
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

            if (env.IsDevelopment())
            {
                // This reads the configuration keys from the secret store.
                // For more details on using the user secret store see http://go.microsoft.com/fwlink/?LinkID=532709
                builder.AddUserSecrets();
            }
            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add Entity Framework services to the services container.
            services.AddEntityFramework()
                .AddSqlServer()
                .AddDbContext<ApplicationContext>(options =>
                    options.UseSqlServer(Configuration["Data:DefaultConnection:ConnectionString"]))
.AddDbContext<HRMContext>(options =>
options.UseSqlServer(Configuration["Data:HRMConstr:ConnectionString"]));

            // Add Identity services to the services container.
            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationContext>()
                .AddDefaultTokenProviders();

            // Add MVC services to the services container.
            services.AddMvc();

            // Uncomment the following line to add Web API services which makes it easier to port Web API 2 controllers.
            // You will also need to add the Microsoft.AspNet.Mvc.WebApiCompatShim package to the 'dependencies' section of project.json.
            // services.AddWebApiConventions();

            // Register application services.
            services.AddTransient<IEmailSender, AuthMessageSender>();
            services.AddTransient<ISmsSender, AuthMessageSender>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }

        // Configure is called after ConfigureServices is called.
        public  void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, UserManager<ApplicationUser> usrMan, RoleManager<IdentityRole> _roleManager   )
        {
            var uow = app.ApplicationServices.GetService<IUnitOfWork>();
            //await Seed(uow, usrMan, _roleManager);
            loggerFactory.MinimumLevel = LogLevel.Information;
            loggerFactory.AddConsole();
            loggerFactory.AddDebug();

            // Configure the HTTP request pipeline.

            // Add the following to the request pipeline only in development environment.
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage(options =>
               {
                   options.EnableAll();
               });

            }
            else
            {
                // Add Error handling middleware which catches all application specific errors and
                // sends the request to the following path or controller action.
                app.UseExceptionHandler("/Home/Error");
            }

            // Add the platform handler to the request pipeline.
            app.UseIISPlatformHandler();

            // Add static files to the request pipeline.
            app.UseStaticFiles();

            // Add cookie-based authentication to the request pipeline.
            app.UseIdentity();

            // Add and configure the options for authentication middleware to the request pipeline.
            // You can add options for middleware as shown below.
            // For more information see http://go.microsoft.com/fwlink/?LinkID=532715
            //app.UseFacebookAuthentication(options =>
            //{
            //    options.AppId = Configuration["Authentication:Facebook:AppId"];
            //    options.AppSecret = Configuration["Authentication:Facebook:AppSecret"];
            //});
            //app.UseGoogleAuthentication(options =>
            //{
            //    options.ClientId = Configuration["Authentication:Google:ClientId"];
            //    options.ClientSecret = Configuration["Authentication:Google:ClientSecret"];
            //});
            //app.UseMicrosoftAccountAuthentication(options =>
            //{
            //    options.ClientId = Configuration["Authentication:MicrosoftAccount:ClientId"];
            //    options.ClientSecret = Configuration["Authentication:MicrosoftAccount:ClientSecret"];
            //});
            //app.UseTwitterAuthentication(options =>
            //{
            //    options.ConsumerKey = Configuration["Authentication:Twitter:ConsumerKey"];
            //    options.ConsumerSecret = Configuration["Authentication:Twitter:ConsumerSecret"];
            //});

            // Add MVC to the request pipeline.
            app.UseMvc(routes =>
            {
                /*
                routes.MapRoute(
    name: "Company",
    template: "{Company}",
    defaults: new { controller = "Home", action = "index" });
    */
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");

                // Uncomment the following line to add a route for porting Web API 2 controllers.
                // routes.MapWebApiRoute("DefaultApi", "api/{controller}/{id?}");
            });
        }

        

        private async Task Seed(IUnitOfWork uow  , UserManager<ApplicationUser> usrMan, RoleManager<IdentityRole> _RoleManager)
        {

            var email = "gabriel.tekin@gmail.com";
            var RoleName = "CustomerAdmin";
            var role = await _RoleManager.FindByNameAsync(RoleName);
            if (role == null)
            {
                role = new IdentityRole { Name = RoleName };
                await _RoleManager.CreateAsync(role);
            }

        RoleName = "superadmin";
            role = await _RoleManager.FindByNameAsync( RoleName );
            if ( role == null)
            {
                role = new IdentityRole { Name = "SuperAdmin" };
                await _RoleManager.CreateAsync(role);
            }
            
            var appUser = await usrMan.FindByEmailAsync(email);
            if (appUser != null)
            {
                var result = await usrMan.DeleteAsync(appUser);
            }
            else 
            {
                appUser = new ApplicationUser() { UserName = email, Email = email };
                var result2 = await usrMan.CreateAsync(appUser, "Shafiro2,");
                if (result2.Succeeded)
                {
                    await usrMan.AddToRoleAsync(appUser,  RoleName ); 
                    var hrmUser = new User()
                    {
                        UserID = "7310209296",
                        FirstName = "Gabriel",
                        LastName = "Tekin",
                        UserName = "gabtek",
                        UserCode = "tekgab",
                        PhoneNumber = "0702385537"
                    };
                    if (uow.UserRepository.GetEagerLoad( w => w.UserID.Equals( hrmUser.UserID, StringComparison.OrdinalIgnoreCase )).FirstOrDefault()   == null )
                    {
                        uow.UserRepository.Insert(hrmUser);
                        uow.Save();
                    }
                    
                }
                else
                {
                    var code = result2.Errors.FirstOrDefault().Code;
                }
            }

            } // method seed 

        // Entry point for the application.
        public static void Main(string[] args) => WebApplication.Run<Startup>(args);

    } // class 
} // namespace