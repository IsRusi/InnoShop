namespace InnoShop.UserManager.Application.DTOs
{
    public class AuthResultDto
    {
        public string Token { get; set; } = string.Empty;
        public string? RefreshToken { get; set; }
        public UserDto? User { get; set; }
    }
}
