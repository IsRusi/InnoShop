using InnoShop.UserManager.Application.Authentication.Commands.ConfirmEmail;
using InnoShop.UserManager.Application.Authentication.Commands.Logout;
using InnoShop.UserManager.Application.Authentication.Commands.Registration;
using InnoShop.UserManager.Application.Authentication.Commands.ResetPassword;
using InnoShop.UserManager.Application.Authentication.Queries.Login;
using InnoShop.UserManager.Application.DTOs;
using InnoShop.UserManager.Application.Users.Commands.SendPasswordResetCode;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InnoShop.UserManager.WebAPI.Controllers
{
    [Route("[controller]")]
    public class AuthenticationController(IMediator mediator) : ControllerBase
    {
        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] RegisterDto dto, CancellationToken cancellationToken = default)
        {
            var command = new RegistrationCommand(dto.FirstName, dto.SecondName, dto.Email, dto.PasswordHash);
            await mediator.Send(command, cancellationToken);
            return Ok(dto);
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginDto dto, CancellationToken cancellationToken = default)
        {
            var query = new LoginQuery(dto.Email ?? string.Empty, dto.Password ?? string.Empty);
            var result = await mediator.Send(query, cancellationToken);
            var response = new
            {
                access_token = result.Token,
                user = result.User
            };
            return Ok(Results.Json(response));
        }

        [HttpPost("verify-email")]
        [AllowAnonymous]
        public async Task<IActionResult> VerifyEmail([FromBody] VerifyEmailRequest request, CancellationToken cancellationToken = default)
        {
            var command = new ConfirmEmailCommand(request.UserId, request.Token);
            await mediator.Send(command, cancellationToken);

            return Ok(new { message = "Email has been successfully verified" });
        }

        [HttpGet("verify-email")]
        [AllowAnonymous]
        public async Task<IActionResult> VerifyEmailGet([FromQuery] Guid id, [FromQuery] string token, CancellationToken cancellationToken = default)
        {
            var command = new ConfirmEmailCommand(id, token);
            await mediator.Send(command, cancellationToken);

            return Ok(new { message = "Email has been successfully verified" });
        }

        [HttpPost("forgotPassword")]
        [AllowAnonymous]
        public async Task<ActionResult> SendPasswordResetCode([FromBody] SendPasswordResetCodeCommand request, CancellationToken cancellationToken = default)
        {
            await mediator.Send(request, cancellationToken);

            return Ok(new { message = "Password reset code has been sent to your email" });
        }

        [HttpPost("reset-password")]
        [AllowAnonymous]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordRequest request, CancellationToken cancellationToken = default)
        {
            var command = new ResetPasswordCommand(request.UserId, request.Token, request.NewPassword);
            await mediator.Send(command, cancellationToken);

            return Ok(new { message = "Password has been successfully reset" });
        }

        [HttpGet("reset-password")]
        [AllowAnonymous]
        public async Task<IActionResult> ResetPasswordGet([FromQuery] Guid userId, [FromQuery] string token, [FromQuery] string newPassword, CancellationToken cancellationToken = default)
        {
            var command = new ResetPasswordCommand(userId, token, newPassword);
            await mediator.Send(command, cancellationToken);

            return Ok(new { message = "Password has been successfully reset" });
        }

        [HttpPost("logout")]
        [Authorize]
        public async Task<IActionResult> Logout([FromBody] LogoutRequest request, CancellationToken cancellationToken = default)
        {
            var command = new LogoutCommand { UserId = request.UserId, RefreshToken = request.RefreshToken };
            await mediator.Send(command, cancellationToken);

            return Ok(new { message = "Successfully logged out" });
        }
    }
}