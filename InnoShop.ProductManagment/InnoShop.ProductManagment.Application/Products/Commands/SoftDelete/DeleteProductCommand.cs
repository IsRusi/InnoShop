using MediatR;

namespace InnoShop.ProductManagment.Application.Products.Commands.SoftDelete
{
    public class DeleteProductCommand : IRequest
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }

        public DeleteProductCommand(Guid id, Guid userId)
        {
            Id = id;
            UserId = userId;
        }
    }
}
