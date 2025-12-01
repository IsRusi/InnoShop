using MediatR;

namespace InnoShop.UserManager.Application.Users.Commands.UpdateUser
{
    public record UpdateUserCommand(
        Guid UserId,
        string FirstName,
        string LastName,
        string Email
    ) : IRequest;
}