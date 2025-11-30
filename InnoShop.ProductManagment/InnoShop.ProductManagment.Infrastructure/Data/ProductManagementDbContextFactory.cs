using InnoShop.ProductManagment.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace InnoShop.ProductManagment.Infrastructure.Data
{
    public class ProductManagementDbContextFactory : IDesignTimeDbContextFactory<ProductManagementDbContext>
    {

        public ProductManagementDbContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true)
                .Build();

            string connectionString = configuration.GetConnectionString("DefaultConnection")!;
            string provider = configuration["DatabaseProvider"]!;

            DbContextOptionsBuilder<ProductManagementDbContext> dbContextOptionsBuilder = new DbContextOptionsBuilder<ProductManagementDbContext>();

            switch (provider)
            {
                case "PostgreSQL": dbContextOptionsBuilder.UseNpgsql(connectionString); break;
                default: throw new Exception($"Unknown database provider: {provider}");
            }

            return new ProductManagementDbContext(dbContextOptionsBuilder.Options);
        }
    }
}
