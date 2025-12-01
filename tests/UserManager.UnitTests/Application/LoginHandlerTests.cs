using FluentAssertions;
using InnoShop.UserManager.Application.Authentication.Queries.Login;
using InnoShop.UserManager.Application.Interfaces.IRepository;
using InnoShop.UserManager.Application.Interfaces.IService;
using InnoShop.UserManager.Domain.Exceptions;
using InnoShop.UserManager.Domain.Interfaces.IService;
using InnoShop.UserManager.Domain.Models;
using Moq;

namespace UserManager.UnitTests.Application
{
    public class LoginHandlerTests
    {
        private readonly Mock<IJwtService> _mockJwtService;
        private readonly Mock<IEmailService> _mockEmailService;
        private readonly Mock<IUserRepository> _mockUserRepository;
        private readonly Mock<IPasswordHasher> _mockPasswordHasher;
        private readonly Mock<IRefreshTokenRepository> _mockRefreshTokenRepository;

        public LoginHandlerTests()
        {
            _mockJwtService = new Mock<IJwtService>();
            _mockEmailService = new Mock<IEmailService>();
            _mockUserRepository = new Mock<IUserRepository>();
            _mockPasswordHasher = new Mock<IPasswordHasher>();
            _mockRefreshTokenRepository = new Mock<IRefreshTokenRepository>();
        }
        [Fact]
        public async Task Handle_WithValidCredentials_ShouldReturnAuthResult()
        {
            var query = new LoginQuery("john@example.com", "password123");
            var userId = Guid.NewGuid();
            var user = new User
            {
                Id = userId,
                FirstName = "John",
                Email = "john@example.com",
                PasswordHash = "hashed",
                IsActive = true,
                IsEmailConfirmed = true,
                Role = "User"
            };

            _mockUserRepository.Setup(x => x.GetByEmailAsync("john@example.com", default))
                .ReturnsAsync((User?)user);

            _mockPasswordHasher.Setup(x => x.VerifyPasswordHash("password123", "hashed"))
                .Returns(true);

            _mockJwtService.Setup(x => x.GenerateAccessTokenAsync(userId, "john@example.com", "User"))
                .ReturnsAsync("access_token");

            _mockJwtService.Setup(x => x.GenerateRefreshTokenAsync())
                .ReturnsAsync("refresh_token");

            _mockRefreshTokenRepository.Setup(x => x.AddAsync(It.IsAny<RefreshToken>(), default))
                .Returns(Task.CompletedTask);

            var handler = new LoginHandler(
                _mockJwtService.Object,
                _mockEmailService.Object,
                _mockUserRepository.Object,
                _mockPasswordHasher.Object,
                _mockRefreshTokenRepository.Object
            );

            var result = await handler.Handle(query, CancellationToken.None);

            result.Should().NotBeNull();
            result.Token.Should().Be("access_token");
        }

        [Fact]
        public async Task Handle_WhenUserNotFound_ShouldThrowException()
        {
            var query = new LoginQuery("notfound@example.com", "password123");

            _mockUserRepository.Setup(x => x.GetByEmailAsync("notfound@example.com", default))
                .ReturnsAsync((User?)null);

            var handler = new LoginHandler(
                _mockJwtService.Object,
                _mockEmailService.Object,
                _mockUserRepository.Object,
                _mockPasswordHasher.Object,
                _mockRefreshTokenRepository.Object
            );

            var act = () => handler.Handle(query, CancellationToken.None);

            await act.Should().ThrowAsync<UserNotFoundException>();
        }

        [Fact]
        public async Task Handle_WhenPasswordInvalid_ShouldThrowException()
        {
            var query = new LoginQuery("john@example.com", "wrongpass");
            var user = new User { Id = Guid.NewGuid(), Email = "john@example.com", PasswordHash = "hashed", IsActive = true, IsEmailConfirmed = true, Role = "User" };

            _mockUserRepository.Setup(x => x.GetByEmailAsync("john@example.com", default))
                .ReturnsAsync(user);

            _mockPasswordHasher.Setup(x => x.VerifyPasswordHash("wrongpass", "hashed"))
                .Returns(false);

            var handler = new LoginHandler(
                _mockJwtService.Object,
                _mockEmailService.Object,
                _mockUserRepository.Object,
                _mockPasswordHasher.Object,
                _mockRefreshTokenRepository.Object
            );

            var act = () => handler.Handle(query, CancellationToken.None);

            await act.Should().ThrowAsync<InvalidCredentialsException>();
        }

        [Fact]
        public async Task Handle_WhenUserNotActive_ShouldThrowException()
        {
            var query = new LoginQuery("john@example.com", "password123");
            var user = new User { Id = Guid.NewGuid(), Email = "john@example.com", PasswordHash = "hashed", IsActive = false, IsEmailConfirmed = true, Role = "User" };

            _mockUserRepository.Setup(x => x.GetByEmailAsync("john@example.com", default))
                .ReturnsAsync(user);

            _mockPasswordHasher.Setup(x => x.VerifyPasswordHash("password123", "hashed"))
                .Returns(true);

            var handler = new LoginHandler(
                _mockJwtService.Object,
                _mockEmailService.Object,
                _mockUserRepository.Object,
                _mockPasswordHasher.Object,
                _mockRefreshTokenRepository.Object
            );

            var act = () => handler.Handle(query, CancellationToken.None);

            await act.Should().ThrowAsync<AccountNotActiveException>();
        }

        [Fact]
        public async Task Handle_WhenEmailNotConfirmed_ShouldThrowException()
        {
            var query = new LoginQuery("john@example.com", "password123");
            var user = new User { Id = Guid.NewGuid(), Email = "john@example.com", PasswordHash = "hashed", IsActive = true, IsEmailConfirmed = false, Role = "User" };

            _mockUserRepository.Setup(x => x.GetByEmailAsync("john@example.com", default))
                .ReturnsAsync(user);

            _mockPasswordHasher.Setup(x => x.VerifyPasswordHash("password123", "hashed"))
                .Returns(true);

            var handler = new LoginHandler(
                _mockJwtService.Object,
                _mockEmailService.Object,
                _mockUserRepository.Object,
                _mockPasswordHasher.Object,
                _mockRefreshTokenRepository.Object
            );

            var act = () => handler.Handle(query, CancellationToken.None);

            await act.Should().ThrowAsync<EmailNotConfirmedException>();
        }
    }
}
