using MediatR;
using InnoShop.ProductManagment.Application.DTOs;

namespace InnoShop.ProductManagment.Application.Products.Queries.GetProductById
{
    public record GetProductByIdQuery(Guid Id) : IRequest<ProductDto?>;
}
