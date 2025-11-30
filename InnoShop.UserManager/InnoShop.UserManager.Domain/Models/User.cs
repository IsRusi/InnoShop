using InnoShop.UserManager.Domain.Exceptions;
using InnoShop.UserManager.Domain.Enum;

namespace InnoShop.UserManager.Domain.Models
{
    public class User:Entity
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public bool IsActive { get; set; } = true;
        public bool IsDeleted { get; set; } = false;
        public bool IsEmailConfirmed { get; set; } = false;
        public string Role { get; set; } = string.Empty;
        //public UserRole Role { get; set; } = UserRole.User;
        public string PasswordHash { get; set; } = string.Empty;

        // Token collections
        public List<RefreshToken> RefreshTokens { get; set; } = new();
        public List<PasswordResetToken> PasswordResetTokens { get; set; } = new();
        public List<EmailConfirmationToken> EmailConfirmationTokens { get; set; } = new();
        public void DeActivate()
        {
            if (IsActive)
            {
                throw new DomainException("Account is active");
            }
            IsActive = false;
        }
        public void SoftDelete()
        {
            if (IsDeleted)
            {
                throw new DomainException("Account is deleted");
            }
            IsDeleted = true;
        }
        public void Activate()
        {
            if (IsActive)
            {
                throw new DomainException("Account is active");
            }
            IsActive = true;
        }
        public void ResetPassword(string password)
        {
            PasswordHash=password;
        }
        public void VerifyEmail()
        {
            if (IsEmailConfirmed)
            {
                throw new DomainException("Email is confirmed");
            }

            IsEmailConfirmed = true;

        }
    }
}
