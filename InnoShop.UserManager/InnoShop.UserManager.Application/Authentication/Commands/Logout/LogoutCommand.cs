using MediatR;

namespace InnoShop.UserManager.Application.Authentication.Commands.Logout
{
    public class LogoutCommand : IRequest
    {
        public Guid UserId { get; set; }
        public string RefreshToken { get; set; } = string.Empty;
    }
}
