using InnoShop.ProductManagment.WebApi.ExceptionHandling;
using InnoShop.ProductManagment.WebApi.Extension;

namespace InnoShop.ProductManagment.WebApi
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
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseMiddleware<GlobalExceptionHandlerMiddleware>();

            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();

            app.Run();
        }
    }
}