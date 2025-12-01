using FluentAssertions;
using InnoShop.UserManager.Application.Authentication.Commands.Registration;
using InnoShop.UserManager.Application.Common.Settings;
using InnoShop.UserManager.Application.Interfaces.IRepository;
using InnoShop.UserManager.Application.Interfaces.IService;
using InnoShop.UserManager.Domain.Exceptions;
using InnoShop.UserManager.Domain.Interfaces.IService;
using InnoShop.UserManager.Domain.Models;
using Moq;
using Microsoft.Extensions.Options;

namespace UserManager.UnitTests.Application
{
    public class RegistrationCommandHandlerTests
    {
        private readonly Mock<IJwtService> _mockJwtService;
        private readonly Mock<IEmailService> _mockEmailService;
        private readonly Mock<IUserRepository> _mockUserRepository;
        private readonly Mock<IPasswordHasher> _mockPasswordHasher;
        private readonly Mock<IEmailConfirmationTokenRepository> _mockEmailConfirmationTokenRepository;

        public RegistrationCommandHandlerTests()
        {
            _mockJwtService = new Mock<IJwtService>();
            _mockEmailService = new Mock<IEmailService>();
            _mockUserRepository = new Mock<IUserRepository>();
            _mockPasswordHasher = new Mock<IPasswordHasher>();
            _mockEmailConfirmationTokenRepository = new Mock<IEmailConfirmationTokenRepository>();
        }
        [Fact]
        public async Task Handle_WithValidData_ShouldCreateUser()
        {
            var command = new RegistrationCommand("John", "Doe", "john@example.com", "password123");

            _mockUserRepository.Setup(x => x.ExistsByEmailAsync("john@example.com", default))
                .ReturnsAsync(false);

            _mockPasswordHasher.Setup(x => x.PasswordHash("password123"))
                .Returns("hashed");

            _mockJwtService.Setup(x => x.GenerateTokenAsync(It.IsAny<User>()))
                .ReturnsAsync("token");

            _mockUserRepository.Setup(x => x.AddAsync(It.IsAny<User>(), default))
                .Returns(Task.CompletedTask);

            _mockEmailConfirmationTokenRepository.Setup(x => x.AddAsync(It.IsAny<EmailConfirmationToken>(), default))
                .Returns(Task.CompletedTask);

            _mockEmailService.Setup(x => x.SendConfirmationCodeAsync(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(Task.CompletedTask);

            var handler = new RegistrationCommandHandler(
                _mockJwtService.Object,
                _mockEmailService.Object,
                _mockUserRepository.Object,
                Options.Create(new AppSettings { FrontendUrl = "http://localhost" }),
                _mockPasswordHasher.Object,
                _mockEmailConfirmationTokenRepository.Object
            );

            await handler.Handle(command, CancellationToken.None);

            _mockUserRepository.Verify(x => x.ExistsByEmailAsync("john@example.com", default), Times.Once);
        }

        [Fact]
        public async Task Handle_WhenEmailExists_ShouldThrowException()
        {
            var command = new RegistrationCommand("John", "Doe", "existing@example.com", "password123");

            _mockUserRepository.Setup(x => x.ExistsByEmailAsync("existing@example.com", default))
                .ReturnsAsync(true);

            var handler = new RegistrationCommandHandler(
                _mockJwtService.Object,
                _mockEmailService.Object,
                _mockUserRepository.Object,
                Options.Create(new AppSettings { FrontendUrl = "http://localhost" }),
                _mockPasswordHasher.Object,
                _mockEmailConfirmationTokenRepository.Object
            );

            var act = () => handler.Handle(command, CancellationToken.None);

            await act.Should().ThrowAsync<UserAlreadyExistsException>();
        }
    }
}
