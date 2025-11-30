using MediatR;

namespace InnoShop.ProductManagment.Application.Products.Commands.AddProduct
{
    public class AddProductCommand : IRequest<Guid>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public Guid UserId { get; set; }

        public AddProductCommand(string name, string description, double price, Guid userId)
        {
            Name = name;
            Description = description;
            Price = price;
            UserId = userId;
        }
    }
}
