using MediatR;

namespace InnoShop.UserManager.Application.Users.Commands.ActivateUser
{
    public record ActivateUserCommand(Guid userId) : IRequest;
}