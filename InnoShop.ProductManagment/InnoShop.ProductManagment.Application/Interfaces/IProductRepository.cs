using InnoShop.ProductManagment.Domain.Models;

namespace InnoShop.ProductManagment.Application.Interfaces
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetProductsAsync(CancellationToken cancellationToken = default);
        Task<IEnumerable<Product>> GetAllByUserIdAsync(Guid userId, CancellationToken cancellationToken = default);
        Task<Product?> GetProductByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task AddProductAsync(Product product, CancellationToken cancellationToken = default);
        Task UpdateProductAsync(Product product, CancellationToken cancellationToken = default);
        Task DeleteProductAsync(Guid id, CancellationToken cancellationToken = default);

    }
}
