using InnoShop.ProductManagment.Application.Common.Constants;
using InnoShop.ProductManagment.Application.Interfaces;
using InnoShop.ProductManagment.Domain.Exceptions;
using MediatR;

namespace InnoShop.ProductManagment.Application.Products.Commands.SoftDelete
{
    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand>
    {
        private readonly IProductRepository _productRepository;

        public DeleteProductCommandHandler(IProductRepository repository)
        {
            _productRepository = repository;
        }

        public async Task Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetProductByIdAsync(request.Id, cancellationToken);
            if (product == null)
                throw new ProductNotFoundException(ErrorMessages.ProductNotFound);

            product.Delete();
            await _productRepository.UpdateProductAsync(product, cancellationToken);
        }
    }
}