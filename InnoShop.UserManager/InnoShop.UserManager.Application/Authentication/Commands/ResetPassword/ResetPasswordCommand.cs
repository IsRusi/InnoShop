using MediatR;

namespace InnoShop.UserManager.Application.Authentication.Commands.ResetPassword
{
    public record ResetPasswordCommand(Guid UserId, string NewPassword) : IRequest;
}
