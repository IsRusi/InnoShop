using InnoShop.UserManager.Application.DTOs;

namespace InnoShop.UserManager.Application.Interfaces.IService
{
    public interface IAuthenticationService
    {
        Task<AuthResultDto?> Login(LoginDto dto);

        Task Registration(RegisterDto registrationDto);
    }
}