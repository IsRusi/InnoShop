using MediatR;
using InnoShop.ProductManagment.Application.DTOs;
using InnoShop.ProductManagment.Application.Interfaces;
using InnoShop.ProductManagment.Domain.Models;

namespace InnoShop.ProductManagment.Application.Products.Queries.GetProductById
{
    public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, ProductDto?>
    {
        private readonly IProductRepository _repository;

        public GetProductByIdQueryHandler(IProductRepository repository)
        {
            _repository = repository;
        }

        public async Task<ProductDto?> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            var product = await _repository.GetProductByIdAsync(request.Id, cancellationToken);
            if (product == null)
                return null;

            return MapToDto(product);
        }

        private ProductDto MapToDto(Product product)
        {
            return new ProductDto
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                IsAvailable = product.IsAvailable,
                UserId = product.UserId,
                CreatedAt = product.CreatedAt
            };
        }
    }
}
