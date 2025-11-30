using InnoShop.UserManager.Application.DTOs;
using MediatR;

namespace InnoShop.UserManager.Application.Users.Queries.GetCurrentUser
{
    public sealed record GetCurrentUserQuery() : IRequest<UserDto>;
}
