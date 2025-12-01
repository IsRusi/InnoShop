using MediatR;

namespace InnoShop.UserManager.Application.Users.Commands.SetAdminUser
{
        public record SetAdminUserCommand(Guid userId) : IRequest;
}
