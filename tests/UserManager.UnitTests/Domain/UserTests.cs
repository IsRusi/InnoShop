using FluentAssertions;
using InnoShop.UserManager.Domain.Exceptions;
using InnoShop.UserManager.Domain.Models;

namespace UserManager.UnitTests.Domain
{
    public class UserTests
    {
        [Fact]
        public void Create_User_ShouldInitializeWithCorrectDefaults()
        {
            // Arrange & Act
            var user = new User
            {
                FirstName = "John",
                LastName = "Doe",
                Email = "john@example.com",
                Role = "User"
            };

            // Assert
            user.FirstName.Should().Be("John");
            user.LastName.Should().Be("Doe");
            user.Email.Should().Be("john@example.com");
            user.Role.Should().Be("User");
            user.IsActive.Should().BeTrue();
            user.IsDeleted.Should().BeFalse();
            user.IsEmailConfirmed.Should().BeFalse();
        }

        [Fact]
        public void ResetPassword_ShouldUpdatePasswordHash()
        {
            // Arrange
            var user = new User { PasswordHash = "old_hash" };
            var newHash = "new_hash";

            // Act
            user.ResetPassword(newHash);

            // Assert
            user.PasswordHash.Should().Be(newHash);
        }

        [Fact]
        public void DeActivate_WhenActive_ShouldSetIsActiveToFalse()
        {
            // Arrange
            var user = new User { IsActive = true };

            // Act
            user.DeActivate();

            // Assert
            user.IsActive.Should().BeFalse();
        }

        [Fact]
        public void DeActivate_WhenInactive_ShouldThrowDomainException()
        {
            // Arrange
            var user = new User { IsActive = false };

            // Act & Assert
            Assert.Throws<DomainException>(() => user.DeActivate());
        }

        [Fact]
        public void Activate_WhenInactive_ShouldSetIsActiveToTrue()
        {
            // Arrange
            var user = new User { IsActive = false };

            // Act
            user.Activate();

            // Assert
            user.IsActive.Should().BeTrue();
        }

        [Fact]
        public void Activate_WhenActive_ShouldThrowDomainException()
        {
            // Arrange
            var user = new User { IsActive = true };

            // Act & Assert
            Assert.Throws<DomainException>(() => user.Activate());
        }

        [Fact]
        public void SoftDelete_WhenNotDeleted_ShouldSetIsDeletedToTrue()
        {
            // Arrange
            var user = new User { IsDeleted = false };

            // Act
            user.SoftDelete();

            // Assert
            user.IsDeleted.Should().BeTrue();
        }

        [Fact]
        public void SoftDelete_WhenAlreadyDeleted_ShouldThrowDomainException()
        {
            // Arrange
            var user = new User { IsDeleted = true };

            // Act & Assert
            Assert.Throws<DomainException>(() => user.SoftDelete());
        }

        [Fact]
        public void VerifyEmail_WhenNotConfirmed_ShouldSetIsEmailConfirmedToTrue()
        {
            // Arrange
            var user = new User { IsEmailConfirmed = false };

            // Act
            user.VerifyEmail();

            // Assert
            user.IsEmailConfirmed.Should().BeTrue();
        }

        [Fact]
        public void VerifyEmail_WhenAlreadyConfirmed_ShouldThrowDomainException()
        {
            // Arrange
            var user = new User { IsEmailConfirmed = true };

            // Act & Assert
            Assert.Throws<DomainException>(() => user.VerifyEmail());
        }

        [Fact]
        public void RefreshTokens_ShouldInitializeAsEmptyList()
        {
            // Arrange & Act
            var user = new User();

            // Assert
            user.RefreshTokens.Should().BeEmpty();
            user.RefreshTokens.Should().BeOfType<List<RefreshToken>>();
        }

        [Fact]
        public void PasswordResetTokens_ShouldInitializeAsEmptyList()
        {
            // Arrange & Act
            var user = new User();

            // Assert
            user.PasswordResetTokens.Should().BeEmpty();
            user.PasswordResetTokens.Should().BeOfType<List<PasswordResetToken>>();
        }

        [Fact]
        public void EmailConfirmationTokens_ShouldInitializeAsEmptyList()
        {
            // Arrange & Act
            var user = new User();

            // Assert
            user.EmailConfirmationTokens.Should().BeEmpty();
            user.EmailConfirmationTokens.Should().BeOfType<List<EmailConfirmationToken>>();
        }
    }
}
