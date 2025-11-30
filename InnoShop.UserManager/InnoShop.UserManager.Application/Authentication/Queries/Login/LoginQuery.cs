using InnoShop.UserManager.Application.DTOs;
using MediatR;

namespace InnoShop.UserManager.Application.Authentication.Queries.Login
{
    public record LoginQuery(
    string Email,
    string Password) : IRequest<AuthResultDto>;
}
