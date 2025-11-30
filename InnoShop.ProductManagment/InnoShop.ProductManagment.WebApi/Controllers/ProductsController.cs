using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using InnoShop.ProductManagment.Application.DTOs;
using InnoShop.ProductManagment.Application.Products.Commands.AddProduct;
using InnoShop.ProductManagment.Application.Products.Commands.UpdateProduct;
using InnoShop.ProductManagment.Application.Products.Commands.SoftDelete;
using InnoShop.ProductManagment.Application.Products.Queries.GetProductById;
using InnoShop.ProductManagment.Application.Products.Queries.GetProducts;

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
        public async Task<IActionResult> GetById(Guid id)
        {
            var query = new GetProductByIdQuery(id);
            var product = await _mediator.Send(query);
            if (product == null)
                return NotFound();
            return Ok(product);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var query = new GetAllProductsQuery();
            var products = await _mediator.Send(query);
            return Ok(products);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create([FromBody] CreateProductRequest request)
        {
            if (request == null)
                return BadRequest();

            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (!Guid.TryParse(userIdClaim, out var userId) || userId == Guid.Empty)
                return Unauthorized();

            var command = new AddProductCommand(request.Name, request.Description, request.Price, userId);
            var productId = await _mediator.Send(command);

            return CreatedAtAction(nameof(GetById), new { id = productId }, null);
        }

        [HttpPatch("{id:guid}")]
        [Authorize]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateProductRequest request)
        {
            if (request == null)
                return BadRequest();

            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (!Guid.TryParse(userIdClaim, out var userId) || userId == Guid.Empty)
                return Unauthorized();

            var command = new UpdateProductCommand(id, request.Name, request.Description, request.Price, request.IsAvailable, userId);
            await _mediator.Send(command);

            return NoContent();
        }

        [HttpDelete("{id:guid}")]
        [Authorize]
        public async Task<IActionResult> Delete(Guid id)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (!Guid.TryParse(userIdClaim, out var userId) || userId == Guid.Empty)
                return Unauthorized();

            var command = new DeleteProductCommand(id, userId);
            await _mediator.Send(command);

            return NoContent();
        }
    }
}
