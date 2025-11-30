using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using InnoShop.ProductManagment.Application;
using InnoShop.ProductManagment.Infrastructure;
using InnoShop.Shared.Application.Interfaces;
using InnoShop.Shared.Infrastructure.Services;

namespace InnoShop.ProductManagment.WebApi.Extension
{
    public static class DependencyInjectionExtension
    {
        public static IServiceCollection AddApplicationDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            var cfg = configuration;

            // Add HTTP Context Accessor FIRST - needed by CurrentUserService
            services.AddHttpContextAccessor();
            
            var connectionString = cfg.GetConnectionString("DefaultConnection");
            services.AddJwtAuthentication(cfg);
            services.AddInfrastructure(cfg);
            services.AddApplication();


            services.AddEndpointsApiExplorer();

            services.AddSwaggerGen(options =>
            {
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    BearerFormat = "JWT",
                    Description = "JWT Authorization header using the Bearer scheme"
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }
                });
            });
            services.AddAuthorization();
            
            // Register Shared services
            services.AddScoped<ICurrentUserService, CurrentUserService>();

            return services;
        }
    }
}
