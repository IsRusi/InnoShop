namespace InnoShop.UserManager.Application.DTOs
{
    public class LogoutRequest
    {
        public Guid UserId { get; set; }
        public string RefreshToken { get; set; } = string.Empty;
    }
}