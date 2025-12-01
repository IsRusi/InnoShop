using InnoShop.ProductManagment.Infrastructure.Options;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace InnoShop.ProductManagment.WebApi.Extension
{
    public static class JwtExtension
    {
        public static IServiceCollection AddJwtAuthentication(this IServiceCollection services, IConfiguration cfg)
        {
            services.Configure<JwtSettings>(cfg.GetSection(JwtSettings.section));
            var jwtSettings = cfg.GetSection(JwtSettings.section).Get<JwtSettings>();

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSettings.Issuer,
                    ValidAudience = jwtSettings.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Key))
                };
            });
            services.AddAuthorization();

            return services;
        }
    }
}