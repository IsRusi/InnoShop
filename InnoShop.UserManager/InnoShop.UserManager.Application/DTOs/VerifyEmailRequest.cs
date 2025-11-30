namespace InnoShop.UserManager.Application.DTOs
{
    public class VerifyEmailRequest
    {
        public Guid UserId { get; set; }
        public string Token { get; set; } = string.Empty;
    }
}
