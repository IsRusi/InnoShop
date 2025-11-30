
using InnoShop.ProductManagment.Application;
using InnoShop.ProductManagment.Infrastructure;
using InnoShop.ProductManagment.WebApi.ExceptionHandling;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace InnoShop.ProductManagment.WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var cfg = builder.Configuration.AddJsonFile("appsettings.json", optional: true).Build();
            
            builder.Services.AddControllers();
            
            // Add services
            var connectionString = cfg.GetConnectionString("DefaultConnection") 
                ?? throw new InvalidOperationException("Connection string not found");
            builder.Services.AddInfrastructure(connectionString);
            builder.Services.AddApplication();

            // Swagger
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // JWT Configuration
            var jwtValues = cfg.GetSection("Jwt");
            var key = jwtValues["Key"] ?? throw new InvalidOperationException("JWT Key not found");
            var issuer = jwtValues["Issuer"] ?? throw new InvalidOperationException("JWT Issuer not found");
            var audience = jwtValues["Audience"] ?? throw new InvalidOperationException("JWT Audience not found");

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = issuer,
                        ValidAudience = audience,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key))
                    };
                });

            var app = builder.Build();

            // Configure the HTTP request pipeline
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            // Global Exception Handler
            app.UseMiddleware<GlobalExceptionHandlerMiddleware>();

            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();

            app.Run();
        }
    }
}
