using InnoShop.ProductManagment.Application.DTOs;
using MediatR;

namespace InnoShop.ProductManagment.Application.Products.Queries.SearchProducts
{
    public record SearchProductsQuery(SearchProductsRequest Request) : IRequest<IEnumerable<ProductDto>>;
}
