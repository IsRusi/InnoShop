
using MediatR;

namespace InnoShop.UserManager.Application.Authentication.Commands.ConfirmEmail
{
    public record ConfirmEmailCommand(Guid userId,string token) : IRequest;
}
