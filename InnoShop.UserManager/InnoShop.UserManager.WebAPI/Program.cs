using InnoShop.UserManager.Application;
using InnoShop.UserManager.Infrastructure;
using InnoShop.UserManager.WebAPI.ExceptionHandling;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace InnoShop.UserManager.WebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var cfg = builder.Configuration.AddJsonFile("appsettings.json", optional: true).Build();
            // Add services to the container.
            builder.Services.AddControllers();
            builder.Services.AddInfrastructure(cfg);
            builder.Services.AddApplication();


            // Swagger

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddHttpContextAccessor();
            var jwtValues = cfg.GetSection("Jwt");
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,//соотношение отправителя/издателя токена
                    ValidateAudience = true, // проверка потребителя токена
                    ValidateLifetime = true, // проверка жизни токена
                    ValidateIssuerSigningKey = true,//валидация ключа безопасности
                    ValidIssuer = jwtValues["Issuer"], // наш издатель
                    ValidAudience = jwtValues["Audience"],//наш потребитель
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtValues["Key"]))// тот самый ключ для signature
                };
    });
            
            // Если нужна аутентификация, раскомментируйте и настройте
            // builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            //     .AddJwtBearer(options => { /* настройки */ });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            // Global Exception Handler
            app.UseMiddleware<GlobalExceptionHandlerMiddleware>();

            app.UseHttpsRedirection();
            app.UseDefaultFiles();
            app.UseStaticFiles();


            app.UseAuthentication();
            app.UseAuthorization();
            
            app.MapControllers();

            app.Run();
        }

    }
}

