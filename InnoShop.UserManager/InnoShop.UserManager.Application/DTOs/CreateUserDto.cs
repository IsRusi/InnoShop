namespace InnoShop.UserManager.Application.DTOs
{
    public class CreateUserDto
    {
        public string Email { get; set; } = string.Empty;
        public bool IsActive { get; set; } = true;
        public bool IsDeleted { get; set; } = false;
        public bool IsEmailConfirmed { get; set; } = false;
        public string Role { get; set; } = string.Empty;
        //public UserRole Role { get; set; } = UserRole.User;
        public string PasswordHash { get; set; } = string.Empty;
    }
}
