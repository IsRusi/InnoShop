using InnoShop.ProductManagment.Application.DTOs;
using InnoShop.ProductManagment.Application.Interfaces;
using InnoShop.ProductManagment.Domain.Models;
using MediatR;

namespace InnoShop.ProductManagment.Application.Products.Queries.SearchProducts
{
    public class SearchProductsQueryHandler : IRequestHandler<SearchProductsQuery, IEnumerable<ProductDto>>
    {
        private readonly IProductRepository _repository;

        public SearchProductsQueryHandler(IProductRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<ProductDto>> Handle(SearchProductsQuery request, CancellationToken cancellationToken)
        {
            var searchParams = new SearchParams()
            {
                SearchTerm = request.Request.SearchTerm,
                MinPrice = request.Request.MinPrice,
                MaxPrice = request.Request.MaxPrice,
                IsAvailable = request.Request.IsAvailable,
                UserId = request.Request.UserId,
            };
            var products = await _repository.SearchAsync(searchParams, cancellationToken);
            return products.Select(MapToDto);
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