using MediatR;

namespace InnoShop.ProductManagment.Application.Products.Commands.UpdateProduct
{
    public class UpdateProductCommand : IRequest
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public bool IsAvailable { get; set; }
        public Guid UserId { get; set; }

        public UpdateProductCommand(Guid id, string name, string description, double price, bool isAvailable, Guid userId)
        {
            Id = id;
            Name = name;
            Description = description;
            Price = price;
            IsAvailable = isAvailable;
            UserId = userId;
        }
    }
}
