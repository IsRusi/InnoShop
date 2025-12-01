using MediatR;

namespace InnoShop.ProductManagment.Application.Products.Commands.AddProduct
{
    public record AddProductCommand(string Name, string Description, double Price, Guid UserId) : IRequest<Guid>;
}