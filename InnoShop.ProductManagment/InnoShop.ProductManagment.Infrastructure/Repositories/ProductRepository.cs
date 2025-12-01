using InnoShop.ProductManagment.Application.Interfaces;
using InnoShop.ProductManagment.Domain.Models;
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
                .Where(p => p.UserId == userId && !p.IsDeleted)
                .ToListAsync(cancellationToken);
        }

        public async Task<Product?> GetProductByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await dbContext.Products
                .FirstOrDefaultAsync(p => p.Id == id && !p.IsDeleted, cancellationToken);
        }

        public async Task<IEnumerable<Product>> GetProductsAsync(CancellationToken cancellationToken = default)
        {
            return await dbContext.Products
                .Where(p => !p.IsDeleted)
                .ToListAsync(cancellationToken);
        }

        public async Task UpdateProductAsync(Product product, CancellationToken cancellationToken = default)
        {
            dbContext.Products.Update(product);
            await dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteProductAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var product = await dbContext.Products
                .FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
            if (product != null)
            {
                product.Delete();
                dbContext.Products.Update(product);
                await dbContext.SaveChangesAsync(cancellationToken);
            }
        }

        public async Task RecoverProductAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var product = await dbContext.Products
                .FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
            if (product != null)
            {
                product.Recover();
                dbContext.Products.Update(product);
                await dbContext.SaveChangesAsync(cancellationToken);
            }
        }
    }
}