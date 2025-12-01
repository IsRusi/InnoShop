using InnoShop.ProductManagment.Application.DTOs;
using InnoShop.ProductManagment.Application.Products.Commands.AddProduct;
using InnoShop.ProductManagment.Application.Products.Commands.RecoverProduct;
using InnoShop.ProductManagment.Application.Products.Commands.SoftDelete;
using InnoShop.ProductManagment.Application.Products.Commands.UpdateProduct;
using InnoShop.ProductManagment.Application.Products.Queries.GetProductById;
using InnoShop.ProductManagment.Application.Products.Queries.GetProducts;
using InnoShop.ProductManagment.Application.Products.Queries.SearchProducts;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace InnoShop.ProductManagment.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken = default)
        {
            var query = new GetProductByIdQuery(id);
            var product = await _mediator.Send(query, cancellationToken);
            if (product == null)
                return NotFound();
            return Ok(product);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken = default)
        {
            var query = new GetAllProductsQuery();
            var products = await _mediator.Send(query, cancellationToken);
            return Ok(products);
        }

         [HttpPost("search")]
        public async Task<IActionResult> SearchByRequest([FromBody] SearchProductsRequest request,CancellationToken cancellationToken = default)
        {
            var query = new SearchProductsQuery(request);
            var products = await _mediator.Send(query, cancellationToken);

            return Ok(products);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create([FromBody] CreateProductRequest request, CancellationToken cancellationToken = default)
        {
            if (request == null)
                return BadRequest();

            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (!Guid.TryParse(userIdClaim, out var userId) || userId == Guid.Empty)
                return Unauthorized();

            var command = new AddProductCommand(request.Name, request.Description, request.Price, userId);
            var productId = await _mediator.Send(command, cancellationToken);

            return CreatedAtAction(nameof(GetById), new { id = productId }, null);
        }

        [HttpPatch("{id:guid}")]
        [Authorize]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateProductRequest request, CancellationToken cancellationToken = default)
        {
            if (request == null)
                return BadRequest();

            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (!Guid.TryParse(userIdClaim, out var userId) || userId == Guid.Empty)
                return Unauthorized();

            var command = new UpdateProductCommand(id, request.Name, request.Description, request.Price, request.IsAvailable, userId);
            await _mediator.Send(command, cancellationToken);

            return NoContent();
        }

        [HttpDelete("{id:guid}")]
        [Authorize]
        public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken = default)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (!Guid.TryParse(userIdClaim, out var userId) || userId == Guid.Empty)
                return Unauthorized();

            var command = new DeleteProductCommand(id);
            await _mediator.Send(command, cancellationToken);

            return NoContent();
        }

        [HttpPost("{id:guid}/recover")]
        [Authorize]
        public async Task<IActionResult> Recover(Guid id, CancellationToken cancellationToken = default)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (!Guid.TryParse(userIdClaim, out var userId) || userId == Guid.Empty)
                return Unauthorized();

            var command = new RecoverProductCommand(userId);
            await _mediator.Send(command, cancellationToken);

            return NoContent();
        }
    }
}