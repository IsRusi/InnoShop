using MediatR;

namespace InnoShop.UserManager.Application.Users.Commands.SendPasswordResetCode
{
    public record SendPasswordResetCodeCommand(string Email) : IRequest;
}
