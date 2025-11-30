using InnoShop.UserManager.Application.DTOs;
using InnoShop.UserManager.Domain.Models;
using MediatR;

namespace InnoShop.UserManager.Application.Users.Queries.GetUsers
{
    public record GetUserQuery(Guid id):IRequest<UserDto>;
}
