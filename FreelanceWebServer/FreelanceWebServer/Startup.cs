using System;
using System.IO;
using System.Text;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;
using FreelanceWebServer.Repositories;
using FreelanceWebServer.Services;
using FreelanceWebServer.Services.JWT;

namespace FreelanceWebServer
{
    public class Startup
    {
        private IConfiguration _configuration;

        public Startup(IConfiguration configuration) => _configuration = configuration;

        public void ConfigureServices(IServiceCollection services) 
        {
            services.Configure<RouteOptions>(options =>
            {
                options.LowercaseUrls = true;
            });

            var accessTokenConfig = _configuration.GetSection("Authentication");

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(accessTokenConfig["AccessTokenSecret"])),
                    ValidIssuer = accessTokenConfig["Issuer"],
                    ValidAudience = accessTokenConfig["Audience"],
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateIssuerSigningKey = true,
                    ClockSkew = TimeSpan.Zero
                };
            });

            services.AddSingleton<IUserRepository, MemoryUserRepository>();
            services.AddSingleton<IOrderRepository, MemoryOrderRepository>();

            services.AddTransient<IAccountService, AccountService>();
            services.AddTransient<IPasswordHasher, BcryptPasswordHasher>();

            services.AddTransient<TokenGenerator>();
            services.AddTransient<AccessTokenGenerator>();
            services.AddTransient<RefreshTokenGenerator>();
            services.AddTransient<RefreshTokenValidator>();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Freelance web server", Version = "v1" });

                var baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
                var commentsFile = Path.Combine(baseDirectory, "FreelanceWebServer.xml");

                c.IncludeXmlComments(commentsFile);
            });

            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            services.AddControllers();
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

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
