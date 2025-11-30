using MediatR;

namespace InnoShop.ProductManagment.Application.Products.Commands.RecoverProduct
{
    public record RecoverProductCommand(Guid Id) : IRequest;
}
