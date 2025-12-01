using MediatR;

namespace InnoShop.UserManager.Application.Authentication.Commands.Registration
{
    public record RegistrationCommand(
  string FirstName, string SecondName, string Email,
  string Password) : IRequest;
}