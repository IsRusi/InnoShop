using InnoShop.UserManager.WebAPI.ExceptionHandling;
using InnoShop.UserManager.WebAPI.Extension;

namespace InnoShop.UserManager.WebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var cfg = builder.Configuration.AddJsonFile("appsettings.json", optional: true).Build();

            builder.Services.AddControllers();
            builder.Services.AddApplicationDependencies(cfg);

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.ApplyMigration();
                app.UseSwaggerDocumentation();
            }

            app.UseMiddleware<GlobalExceptionHandlerMiddleware>();

            if (!app.Environment.IsDevelopment())
            {
                app.UseHttpsRedirection();
            }

            app.UseDefaultFiles();
            app.UseStaticFiles();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}