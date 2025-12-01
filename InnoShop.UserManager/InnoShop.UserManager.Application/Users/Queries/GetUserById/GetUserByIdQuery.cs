using InnoShop.UserManager.Application.DTOs;
using MediatR;

namespace InnoShop.UserManager.Application.Users.Queries.GetUserById
{
    public record GetUserByIdQuery(Guid id) : IRequest<UserDto>;
}