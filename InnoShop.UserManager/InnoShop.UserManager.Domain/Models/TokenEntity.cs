namespace InnoShop.UserManager.Domain.Models
{
    public class TokenEntity : Entity
    {
        public Guid UserId { get; set; }
        public string Token { get; set; } = string.Empty;
        public DateTime ExpirationDate { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsExpired => DateTime.UtcNow >= ExpirationDate;
    }
}