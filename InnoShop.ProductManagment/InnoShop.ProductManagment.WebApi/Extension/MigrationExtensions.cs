using InnoShop.ProductManagment.Infrastructure.Data;

using Microsoft.EntityFrameworkCore;

namespace InnoShop.ProductManagment.WebApi.Extension
{
    public static class MigrationExtensions
    {
        public static void ApplyMigration(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<ProductManagementDbContext>();
            db.Database.Migrate();
        }
    }
}