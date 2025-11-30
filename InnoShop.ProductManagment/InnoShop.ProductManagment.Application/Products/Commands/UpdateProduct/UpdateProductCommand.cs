using MediatR;

namespace InnoShop.ProductManagment.Application.Products.Commands.UpdateProduct
{
    public record UpdateProductCommand(Guid id, string name, string description, double price, bool isAvailable, Guid userId) : IRequest;
}
