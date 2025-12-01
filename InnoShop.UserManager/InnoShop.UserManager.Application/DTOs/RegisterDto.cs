namespace InnoShop.UserManager.Application.DTOs
{
    public class RegisterDto
    {
        public string Email { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string SecondName { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
    }
}