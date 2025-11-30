using InnoShop.UserManager.Application.Common.Constants;
using InnoShop.UserManager.Application.Common.Settings;
using InnoShop.UserManager.Application.Interfaces.IRepository;
using InnoShop.UserManager.Application.Interfaces.IService;
using InnoShop.UserManager.Domain.Exceptions;
using InnoShop.UserManager.Domain.Interfaces.IService;
using InnoShop.UserManager.Domain.Models;
using MediatR;
using Microsoft.Extensions.Options;

namespace InnoShop.UserManager.Application.Authentication.Commands.Registration
{
    public class RegistrationCommandHandler:IRequestHandler<RegistrationCommand>
    {
        private readonly IJwtService _jwtService;
        private readonly IEmailService _emailService;
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IOptions<AppSettings> _appSettings;
        private readonly IEmailConfirmationTokenRepository _emailConfirmationTokenRepository;

        public RegistrationCommandHandler(
            IJwtService jwtService,
            IEmailService emailService,
            IUserRepository userRepository,
           IOptions<AppSettings> appSettings,
            IPasswordHasher passwordHasher,
            IEmailConfirmationTokenRepository emailConfirmationTokenRepository)
        {
            _jwtService = jwtService;
            _emailService = emailService;
            _userRepository = userRepository;
            _appSettings = appSettings;
            _passwordHasher = passwordHasher;
            _emailConfirmationTokenRepository = emailConfirmationTokenRepository;
        }

        public async Task Handle(RegistrationCommand request, CancellationToken cancellationToken)
        {
            var isExistsUser = await _userRepository.ExistsByEmailAsync(request.Email);
            if (isExistsUser)
                throw new UserAlreadyExistsException(ErrorMessages.EmailAlreadyExists);

            var user = new User
            {
                FirstName = request.FirstName,
                LastName = request.SecondName,
                Email = request.Email,
                Role = "User"
            };

            user.PasswordHash = _passwordHasher.PasswordHash(request.Password);
            await _userRepository.AddAsync(user);

            var confirmationToken = await _jwtService.GenerateTokenAsync(user);
            var emailConfirmTokenEntity = EmailConfirmationToken.Create(user.Id, confirmationToken, DateTime.UtcNow.AddHours(24));
            await _emailConfirmationTokenRepository.AddAsync(emailConfirmTokenEntity, cancellationToken);

            var confirmationLink = $"{_appSettings.Value.FrontendUrl}/authentication/verify-email?id={user.Id}&token={confirmationToken}";
            await _emailService.SendConfirmationCodeAsync(user.Email, confirmationLink);
        }
    }
}
