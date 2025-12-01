using InnoShop.UserManager.Application.Common.Constants;
using InnoShop.UserManager.Application.Common.Settings;
using InnoShop.UserManager.Application.Interfaces.IRepository;
using InnoShop.UserManager.Domain.Exceptions;
using InnoShop.UserManager.Domain.Interfaces.IService;
using InnoShop.UserManager.Domain.Models;
using MediatR;
using Microsoft.Extensions.Options;

namespace InnoShop.UserManager.Application.Users.Commands.SendPasswordResetCode
{
    public class SendPasswordResetCodeCommandHandler : IRequestHandler<SendPasswordResetCodeCommand>
    {
        private readonly IUserRepository _userRepository;
        private readonly IEmailService _emailService;
        private readonly IPasswordResetTokenRepository _passwordResetTokenRepository;
        private readonly IOptions<AppSettings> _appSettings;
        private readonly IOptions<PasswordResetTokenSettings> _passwordResetTokenSettings;

        public SendPasswordResetCodeCommandHandler(
            IUserRepository userRepository,
            IEmailService emailService,
            IPasswordResetTokenRepository passwordResetTokenRepository,
            IOptions<AppSettings> appSettings,
            IOptions<PasswordResetTokenSettings> passwordResetTokenSettings)
        {
            _userRepository = userRepository;
            _emailService = emailService;
            _passwordResetTokenRepository = passwordResetTokenRepository;
            _appSettings = appSettings;
            _passwordResetTokenSettings = passwordResetTokenSettings;
        }

        public async Task Handle(SendPasswordResetCodeCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByEmailAsync(request.Email);
            if (user == null)
            {
                throw new UserNotFoundException(ErrorMessages.UserNotFound);
            }

            var token = GenerateSecureToken();
            var expiresAt = DateTime.UtcNow.AddMinutes(_passwordResetTokenSettings.Value.ExpireMinutes);

            var resetToken = PasswordResetToken.Create(user.Id, token, expiresAt);

            await _passwordResetTokenRepository.AddAsync(resetToken);

            var resetLink = $"{_appSettings.Value.FrontendUrl}/reset-password?id={user.Id}&token={token}";

            await _emailService.SendResetCodeAsync(user.Email, token, resetLink);
        }

        private string GenerateSecureToken()
        {
            var randomBytes = new byte[32];
            using (var rng = System.Security.Cryptography.RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomBytes);
            }
            return Convert.ToBase64String(randomBytes);
        }
    }
}