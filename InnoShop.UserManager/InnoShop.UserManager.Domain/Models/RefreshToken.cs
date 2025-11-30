namespace InnoShop.UserManager.Domain.Models
{
    public class RefreshToken : TokenEntity
    {
        public bool IsRevoked { get; set; } = false;
        public DateTime? RevokedAt { get; set; }

        public User? User { get; set; }

        private RefreshToken() { }

        public static RefreshToken Create(Guid userId, string token, DateTime expirationDate)
        {
            return new RefreshToken
            {
                UserId = userId,
                Token = token,
                ExpirationDate = expirationDate,
                CreatedAt = DateTime.UtcNow,
                IsRevoked = false
            };
        }

        public bool IsValid()
        {
            return !IsRevoked && DateTime.UtcNow < ExpirationDate;
        }

        public void Revoke()
        {
            IsRevoked = true;
            RevokedAt = DateTime.UtcNow;
        }
    }
}
