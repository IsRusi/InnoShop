using InnoShop.ProductManagment.Application.Interfaces;
using MediatR;

namespace InnoShop.ProductManagment.Application.Products.Commands.RecoverProduct
{
    public class RecoverProductCommandHandler : IRequestHandler<RecoverProductCommand>
    {
        private readonly IProductRepository _productRepository;

        public RecoverProductCommandHandler(IProductRepository repository)
        {
            _productRepository = repository;
        }

        public async Task Handle(RecoverProductCommand request, CancellationToken cancellationToken)
        {
            var products = await _productRepository.GetAllByUserIdAsync(request.Id, cancellationToken);

            foreach (var product in products)
            {
                product.Recover();
                await _productRepository.UpdateProductAsync(product, cancellationToken);
            }
        }
    }
}