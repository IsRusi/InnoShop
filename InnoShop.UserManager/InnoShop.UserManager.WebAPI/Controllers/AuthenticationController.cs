using InnoShop.UserManager.Application.DTOs;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using InnoShop.UserManager.Application.Authentication.Commands;
using InnoShop.UserManager.Application.Authentication.Queries;
using InnoShop.UserManager.Application.Authentication.Queries.Login;
using InnoShop.UserManager.Application.Authentication.Commands.Registration;
using InnoShop.UserManager.Application.Authentication.Commands.ConfirmEmail;

namespace InnoShop.UserManager.WebAPI.Controllers
{
    [AllowAnonymous]
    [Route("[controller]")]
    public class AuthenticationController(IMediator meadiator) : ControllerBase
    {
       
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto dto)
        {
            var command = new RegistrationCommand(dto.FirstName,dto.SecondName,dto.Email,dto.PasswordHash);
            await meadiator.Send(command);
            return Ok(dto);
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            var query = new LoginQuery(dto.Email,dto.Password);
            var result = await meadiator.Send(query);
            var response = new
            {
                access_token = result.Token,
                user = result.User
                };
            return Ok(Results.Json(response));
        }
        [HttpGet("verify-email", Name = "VerifyEmailRoute")]
        public async Task<IActionResult> VerifyEmail([FromQuery] Guid userId, [FromQuery] string token)
        {
            var command = new ConfirmEmailCommand(userId, token);
            await meadiator.Send(command);
            
            return Ok("успех");
        }
        [HttpGet("verify-email", Name = "VerifyEmailRoute")]
        public async Task<IActionResult> ResetPassword([FromQuery] Guid userId, [FromQuery] string token)
        {
            var command = new ConfirmEmailCommand(userId, token);
            await meadiator.Send(command);

            return Ok("успех");
        }
    }
}
