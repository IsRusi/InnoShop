using MediatR;

namespace InnoShop.ProductManagment.Application.Products.Commands.SoftDelete
{
    public record DeleteProductCommand(Guid Id) : IRequest;
}