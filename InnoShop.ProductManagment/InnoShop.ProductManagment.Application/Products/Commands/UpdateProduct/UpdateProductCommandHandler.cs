using MediatR;
using InnoShop.ProductManagment.Application.Interfaces;
using InnoShop.ProductManagment.Domain.Exceptions;
using InnoShop.ProductManagment.Application.Common.Constants;

namespace InnoShop.ProductManagment.Application.Products.Commands.UpdateProduct
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand>
    {
        private readonly IProductRepository _repository;

        public UpdateProductCommandHandler(IProductRepository repository)
        {
            _repository = repository;
        }

        public async Task Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var product = await _repository.GetProductByIdAsync(request.id, cancellationToken);
            if (product == null)
                throw new ProductNotFoundException(ErrorMessages.ProductNotFound);

            if (product.UserId != request.userId)
                throw new UnauthorizedProductAccessException(ErrorMessages.UnauthorizedAccess);

            product.ChangeName(request.name);
            product.ChangeDescription(request.description);
            product.ChangePrice(request.price);

            if (request.isAvailable && !product.IsAvailable)
                product.SetAvailable();
            else if (!request.isAvailable && product.IsAvailable)
                product.SetUnavailable();

            await _repository.UpdateProductAsync(product, cancellationToken);
        }
    }
}
