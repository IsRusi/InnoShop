using MediatR;
using InnoShop.ProductManagment.Application.DTOs;

namespace InnoShop.ProductManagment.Application.Products.Queries.GetProducts
{
    public record GetAllProductsQuery() : IRequest<IEnumerable<ProductDto>>;
}
