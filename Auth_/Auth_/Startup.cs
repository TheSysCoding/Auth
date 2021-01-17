using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Auth_
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            //Añadimos un esquema de autenticacion por cookies y lo establecemos por defecto
            services.AddAuthentication("CookieAuth").AddCookie("CookieAuth", options =>
            {
                options.Cookie.Name = "CookieAuthByCristian";
                options.Cookie.MaxAge = TimeSpan.FromMinutes(30);
                options.LoginPath = "/home/login";
            });
            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            //Authenticate ¿Quien eres? 
            app.UseAuthentication();
            //¿Esta permitido que entres a donde quieres entrar? 
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                //home/index
                //home/secure
                
                endpoints.MapDefaultControllerRoute();
            });
        }
    }
}
