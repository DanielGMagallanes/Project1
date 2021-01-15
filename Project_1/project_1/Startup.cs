using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using project_1.Security;
using Microsoft.AspNetCore.Authentication;
using RepositoryLayer;
using BusinessLayer;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace project_1
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
          
            services.AddAuthorization( options =>
            {
                options.AddPolicy("IsAdmin", policyBuilder => policyBuilder.RequireClaim("IsAdmin"));
                options.AddPolicy("CanAccessStore", policyBuilder => policyBuilder.Requirements.Add(new CanAccessStore()));
            });
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie();

            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });
            services.AddDistributedMemoryCache();
            services.AddSession(options => {
                options.IdleTimeout = TimeSpan.FromMinutes(60);//You can set Time   
            });
            services.AddControllersWithViews();
            services.AddSingleton<IAuthorizationHandler, CanAccessUserHandler>();
            services.AddScoped<AuthenticationService>();
            services.AddScoped<StoreAppContext>();
            services.AddScoped<StoreAppRepsitoryLayer>();
            services.AddScoped<MapperClass>();
            services.AddScoped<StoreLevelPrograms>();
            
            services.AddRazorPages();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
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
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();
            app.UseSession();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=MainPage}/{id?}");
            });
        }
    }
}
