using InnoShop.ProductManagment.Domain.Models;
using InnoShop.ProductManagment.Application.Interfaces;
using InnoShop.ProductManagment.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace InnoShop.ProductManagment.Infrastructure.Repositories
{
    public class ProductRepository(ProductManagementDbContext dbContext) : IProductRepository
    {
        public async Task AddProductAsync(Product product, CancellationToken cancellationToken = default)
        {
            await dbContext.Products.AddAsync(product, cancellationToken);
            await dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task<IEnumerable<Product>> GetAllByUserIdAsync(Guid userId, CancellationToken cancellationToken = default)
        {
            return await dbContext.Products
                .Where(p => p.UserId == userId)
                .ToListAsync(cancellationToken);
        }

        public async Task<Product?> GetProductByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await dbContext.Products
                .FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
        }

        public async Task<IEnumerable<Product>> GetProductsAsync(CancellationToken cancellationToken = default)
        {
            return await dbContext.Products.ToListAsync(cancellationToken);
        }

        public async Task UpdateProductAsync(Product product, CancellationToken cancellationToken = default)
        {
            dbContext.Products.Update(product);
            await dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteProductAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var product = await GetProductByIdAsync(id, cancellationToken);
            if (product != null)
            {
                dbContext.Products.Remove(product);
                await dbContext.SaveChangesAsync(cancellationToken);
            }
        }

        public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            await dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}

