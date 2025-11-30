using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using InnoShop.UserManager.Application.DTOs;
using InnoShop.UserManager.Application.Users.Commands.AddUser;
using InnoShop.UserManager.Application.Users.Commands.ActivateUser;
using InnoShop.UserManager.Application.Users.Commands.DeactivateUser;
using InnoShop.UserManager.Application.Users.Commands.DeleteUser;
using InnoShop.UserManager.Application.Users.Queries.GetUsers;

namespace InnoShop.UserManager.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UsersController(IMediator mediator) => _mediator = mediator;

        [HttpGet("{userId:guid}")]
        public async Task<IActionResult> Get(Guid userId)
        {
            var query = new GetUserQuery(userId);
            var user = await _mediator.Send(query);
            if (user == null) return NotFound();
            return Ok(user);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([FromBody] UserDto userDto)
        {
            if (userDto == null) return BadRequest();
            userDto.IsDeleted = false;
            userDto.IsEmailConfirmed = false;
            var command = new AddUserCommand(userDto);
            await _mediator.Send(command);

            return CreatedAtAction(nameof(Get), new { id = userDto.Id }, null);
        }

        [HttpPatch("{userId:guid}/activate")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Activate(Guid id)
        {
            await _mediator.Send(new ActivateUserCommand(id));
            return NoContent();
        }

        [HttpPatch("{userId:guid}/deactivate")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Deactivate(Guid id)
        {
            await _mediator.Send(new DeactivateUserCommand(id));
            return NoContent();
        }

        [HttpDelete("{userId:guid}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _mediator.Send(new DeleteUserCommand(id));
            return NoContent();
        }
    }
}
