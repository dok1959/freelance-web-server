using System;
using System.IO;
using System.Text;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using FreelanceWebServer.Repositories;
using FreelanceWebServer.Services;
using FreelanceWebServer.Services.JWT;
using FreelanceWebServer.Data.PostgreSQL;
using FreelanceWebServer.Repositories.OrderRepository;
using FreelanceWebServer.Repositories.UserRepository;

namespace FreelanceWebServer
{
    public class Startup
    {
        private IConfiguration _configuration;

        public Startup(IConfiguration configuration) => _configuration = configuration;

        public void ConfigureServices(IServiceCollection services) 
        {
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

            services.AddSingleton<PostgresDBContext>();

            services.AddScoped<IUserRepository, PostgresUserRepository>();
            services.AddScoped<IRoleRepository, PostgresRoleRepository>();
            services.AddScoped<IOrderRepository, PostgresOrderRepository>();

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

            services.AddAutoMapper(typeof(MappingProfile));

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
