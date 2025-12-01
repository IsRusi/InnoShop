using InnoShop.ProductManagment.Application.DTOs;
using MediatR;

namespace InnoShop.ProductManagment.Application.Products.Queries.GetProducts
{
    public record GetAllProductsQuery() : IRequest<IEnumerable<ProductDto>>;
}