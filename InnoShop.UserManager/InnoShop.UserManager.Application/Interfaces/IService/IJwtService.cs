using InnoShop.UserManager.Domain.Models;
using System.Security.Claims;

namespace InnoShop.UserManager.Domain.Interfaces.IService
{
    public interface IJwtService
    {
        ClaimsPrincipal? ValidateToken(string token);

        Task<string> GenerateTokenAsync(User user);

        Task<string> GenerateAccessTokenAsync(Guid userId, string email, string role);

        Task<string> GenerateRefreshTokenAsync();
    }
}