using InnoShop.UserManager.Application.DTOs;
using InnoShop.UserManager.Application.Users.Commands.ActivateUser;
using InnoShop.UserManager.Application.Users.Commands.AddUser;
using InnoShop.UserManager.Application.Users.Commands.DeactivateUser;
using InnoShop.UserManager.Application.Users.Commands.SendPasswordResetCode;
using InnoShop.UserManager.Application.Users.Queries.GetUsers;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InnoShop.UserManager.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UsersController(IMediator mediator) => _mediator = mediator;

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> Get(Guid id, CancellationToken cancellationToken = default)
        {
            var query = new GetUserQuery(id);
            var user = await _mediator.Send(query, cancellationToken);
            if (user == null) return NotFound();
            return Ok(user);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([FromBody] UserDto userDto, CancellationToken cancellationToken = default)
        {
            if (userDto == null) return BadRequest();
            var command = new AddUserCommand(userDto);
            await _mediator.Send(command, cancellationToken);

            return CreatedAtAction(nameof(Get), new { id = userDto.Id }, null);
        }

        [HttpPatch("{id:guid}/activate")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Activate(Guid id, CancellationToken cancellationToken = default)
        {
            await _mediator.Send(new ActivateUserCommand(id), cancellationToken);
            return NoContent();
        }

        [HttpPatch("{id:guid}/deactivate")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Deactivate(Guid id, CancellationToken cancellationToken = default)
        {
            await _mediator.Send(new DeactivateUserCommand(id), cancellationToken);
            return NoContent();
        }

        [HttpPost("send-password-reset-code")]
        public async Task<IActionResult> SendPasswordResetCode([FromBody] SendPasswordResetCodeCommand request, CancellationToken cancellationToken = default)
        {
            if (request == null || string.IsNullOrWhiteSpace(request.Email))
                return BadRequest("Email is required");

            var command = new SendPasswordResetCodeCommand(request.Email);
            await _mediator.Send(command, cancellationToken);

            return Ok(new { message = "Password reset code has been sent to your email" });
        }
    }
}


