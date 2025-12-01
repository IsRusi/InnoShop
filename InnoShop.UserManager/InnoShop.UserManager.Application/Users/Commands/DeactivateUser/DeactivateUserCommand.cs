using MediatR;

namespace InnoShop.UserManager.Application.Users.Commands.DeactivateUser
{
    public record DeactivateUserCommand(Guid userId) : IRequest;
}