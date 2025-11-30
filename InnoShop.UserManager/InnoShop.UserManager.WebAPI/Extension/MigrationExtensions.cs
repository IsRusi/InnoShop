using InnoShop.UserManager.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;

namespace InnoShop.UserManager.WebAPI.Extension
{
    public static class MigrationExtensions
    {
        public static void ApplyMigration(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<UserContext>();
            db.Database.Migrate();
        }
    }
}