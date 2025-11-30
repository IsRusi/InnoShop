using MediatR;
using InnoShop.ProductManagment.Application.DTOs;

namespace InnoShop.ProductManagment.Application.Products.Queries.GetProductById
{
    public class GetProductByIdQuery : IRequest<ProductDto?>
    {
        public Guid Id { get; set; }

        public GetProductByIdQuery(Guid id)
        {
            Id = id;
        }
    }
}
