namespace InnoShop.UserManager.Domain.Models
{
    public class PasswordResetToken : TokenEntity
    {

        public bool IsUsed { get; set; } = false;
        public DateTime? UsedAt { get; set; }

        public User? User { get; set; }

        private PasswordResetToken() { }

        public static PasswordResetToken Create(Guid userId, string token, DateTime expirationDate)
        {
            return new PasswordResetToken
            {
                UserId = userId,
                Token = token,
                ExpirationDate = expirationDate,
                CreatedAt = DateTime.UtcNow,
                IsUsed = false
            };
        }

        public bool IsValid()
        {
            return !IsUsed && DateTime.UtcNow < ExpirationDate;
        }

        public void MarkAsUsed()
        {
            IsUsed = true;
            UsedAt = DateTime.UtcNow;
        }
    }
}
