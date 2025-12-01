using InnoShop.UserManager.Application.DTOs;
using MediatR;

namespace InnoShop.UserManager.Application.Users.Queries.GetUsers
{
    public record GetUserQuery(Guid id) : IRequest<UserDto>;
}