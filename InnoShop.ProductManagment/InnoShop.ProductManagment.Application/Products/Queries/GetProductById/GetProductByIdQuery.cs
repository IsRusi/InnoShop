using InnoShop.ProductManagment.Application.DTOs;
using MediatR;

namespace InnoShop.ProductManagment.Application.Products.Queries.GetProductById
{
    public record GetProductByIdQuery(Guid Id) : IRequest<ProductDto?>;
}