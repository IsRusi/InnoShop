using InnoShop.UserManager.Domain.Common.Constants;
using InnoShop.UserManager.Domain.Exceptions;

namespace InnoShop.UserManager.Domain.Models
{
    public class User : Entity
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public bool IsActive { get; set; } = true;
        public bool IsDeleted { get; set; } = false;
        public bool IsEmailConfirmed { get; set; } = false;
        public string Role { get; set; } = string.Empty;

        public string PasswordHash { get; set; } = string.Empty;

        public List<RefreshToken> RefreshTokens { get; set; } = new();
        public List<PasswordResetToken> PasswordResetTokens { get; set; } = new();
        public List<EmailConfirmationToken> EmailConfirmationTokens { get; set; } = new();

        public void ResetPassword(string password)
        {
            PasswordHash = password;
        }

        public void DeActivate()
        {
            if (!IsActive)
            {
                throw new DomainException(ErrorMessages.UserAlreadyInactive);
            }
            IsActive = false;
        }

        public void SoftDelete()
        {
            if (IsDeleted)
            {
                throw new DomainException(ErrorMessages.UserAlreadyDeleted);
            }
            IsDeleted = true;
        }

        public void Activate()
        {
            if (IsActive)
            {
                throw new DomainException(ErrorMessages.UserAlreadyActive);
            }
            IsActive = true;
        }

        public void VerifyEmail()
        {
            if (IsEmailConfirmed)
            {
                throw new DomainException(ErrorMessages.EmailAlreadyConfirmed);
            }
            IsEmailConfirmed = true;
        }
    }
}