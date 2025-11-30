using MediatR;
using InnoShop.ProductManagment.Application.DTOs;

namespace InnoShop.ProductManagment.Application.Products.Queries.GetProducts
{
    public class GetAllProductsQuery : IRequest<IEnumerable<ProductDto>>
    {
    }
}
