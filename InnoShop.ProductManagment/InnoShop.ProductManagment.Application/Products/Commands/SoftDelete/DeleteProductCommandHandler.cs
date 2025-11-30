using MediatR;
using InnoShop.ProductManagment.Application.Common;
using InnoShop.ProductManagment.Application.Interfaces;
using InnoShop.ProductManagment.Domain.Exceptions;

namespace InnoShop.ProductManagment.Application.Products.Commands.SoftDelete
{
    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand>
    {
        private readonly IProductRepository _repository;

        public DeleteProductCommandHandler(IProductRepository repository)
        {
            _repository = repository;
        }

        public async Task Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var product = await _repository.GetProductByIdAsync(request.Id, cancellationToken);
            if (product == null)
                throw new ProductNotFoundException(ErrorMessages.ProductNotFound);

            if (product.UserId != request.UserId)
                throw new UnauthorizedProductAccessException(ErrorMessages.UnauthorizedAccess);

            await _repository.DeleteProductAsync(request.Id, cancellationToken);
        }
    }
}
