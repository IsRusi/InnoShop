using InnoShop.ProductManagment.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace InnoShop.ProductManagment.Infrastructure.Data
{
    public class ProductManagementDbContext : DbContext
    {
        public ProductManagementDbContext(DbContextOptions<ProductManagementDbContext> options) : base(options) { }

        public DbSet<Product> Products { get; set; }


    }
}
