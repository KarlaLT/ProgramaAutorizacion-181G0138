using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using AutorizacionConGalletitas.Models;
using Microsoft.EntityFrameworkCore;

namespace AutorizacionConGalletitas
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddAuthentication(
                options =>
                {
                    options.DefaultSignInScheme 
                        = CookieAuthenticationDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme
                        = CookieAuthenticationDefaults.AuthenticationScheme;
                    options.DefaultAuthenticateScheme
                        = CookieAuthenticationDefaults.AuthenticationScheme;
                }
                ).AddCookie( options=>
                {
                    options.ExpireTimeSpan = TimeSpan.FromMinutes(1);
                    options.SlidingExpiration = true;
                    options.LoginPath = "/Home/Login";
                });
           
            services.AddDbContext<rolesContext>(options =>
            {
                var connectionString = "server=localhost;user=root;password=root;database=roles";
                options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
            });
            services.AddMvc();
            services.AddMvc(options =>
                options.EnableEndpointRouting = false);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseAuthentication();
            app.UseMvcWithDefaultRoute();
        }
    }
}
