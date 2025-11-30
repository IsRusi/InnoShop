using InnoShop.UserManager.Application.DTOs;
using MediatR;

namespace InnoShop.UserManager.Application.Users.Commands.AddUser
{
    public record AddUserCommand(UserDto userDto):IRequest;
}
