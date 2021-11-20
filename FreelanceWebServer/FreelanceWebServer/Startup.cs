using System;
using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using FreelanceWebServer.Repositories;
using FreelanceWebServer.Services;

namespace FreelanceWebServer
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services) 
        {
            services.AddTransient<IUserRepository, MemoryUserRepository>();
            services.AddTransient<IAccountService, AccountService>();

            services.AddControllersWithViews();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Freelance web server", Version = "v1" });

                var baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
                var commentsFile = Path.Combine(baseDirectory, "FreelanceWebServer.xml");

                c.IncludeXmlComments(commentsFile);
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
                endpoints.MapControllers();
            });
        }
    }
}
