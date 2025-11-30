using MediatR;
using InnoShop.ProductManagment.Application.Interfaces;
using InnoShop.ProductManagment.Domain.Models;

namespace InnoShop.ProductManagment.Application.Products.Commands.AddProduct
{
    public class AddProductCommandHandler : IRequestHandler<AddProductCommand, Guid>
    {
        private readonly IProductRepository _repository;

        public AddProductCommandHandler(IProductRepository repository)
        {
            _repository = repository;
        }

        public async Task<Guid> Handle(AddProductCommand request, CancellationToken cancellationToken)
        {
            var product = new Product(request.Name, request.Description, request.UserId, request.Price);
            await _repository.AddProductAsync(product, cancellationToken);
            return product.Id;
        }
    }
}
