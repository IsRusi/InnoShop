using MediatR;
using InnoShop.ProductManagment.Application.Common;
using InnoShop.ProductManagment.Application.Interfaces;
using InnoShop.ProductManagment.Domain.Exceptions;

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
            var product = await _repository.GetProductByIdAsync(request.Id, cancellationToken);
            if (product == null)
                throw new ProductNotFoundException(ErrorMessages.ProductNotFound);

            if (product.UserId != request.UserId)
                throw new UnauthorizedProductAccessException(ErrorMessages.UnauthorizedAccess);

            product.ChangeName(request.Name);
            product.ChangeDescription(request.Description);
            product.ChangePrice(request.Price);

            if (request.IsAvailable && !product.IsAvailable)
                product.SetAvailable();
            else if (!request.IsAvailable && product.IsAvailable)
                product.SetUnavailable();

            await _repository.UpdateProductAsync(product, cancellationToken);
        }
    }
}
