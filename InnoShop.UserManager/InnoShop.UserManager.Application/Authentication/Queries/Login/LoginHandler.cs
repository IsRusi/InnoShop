using InnoShop.UserManager.Application.Common.Constants;
using InnoShop.UserManager.Application.DTOs;
using InnoShop.UserManager.Application.Interfaces.IRepository;
using InnoShop.UserManager.Application.Interfaces.IService;
using InnoShop.UserManager.Domain.Exceptions;
using InnoShop.UserManager.Domain.Interfaces.IService;
using InnoShop.UserManager.Domain.Models;
using MediatR;

namespace InnoShop.UserManager.Application.Authentication.Queries.Login
{
    public class LoginHandler : IRequestHandler<LoginQuery, AuthResultDto>
    {
        private readonly IJwtService _jwtService;
        private readonly IEmailService _emailService;
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IRefreshTokenRepository _refreshTokenRepository;

        public LoginHandler(
            IJwtService jwtService,
            IEmailService emailService,
            IUserRepository userRepository,
            IPasswordHasher passwordHasher,
            IRefreshTokenRepository refreshTokenRepository)
        {
            _jwtService = jwtService;
            _emailService = emailService;
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            _refreshTokenRepository = refreshTokenRepository;
        }

        public async Task<AuthResultDto> Handle(LoginQuery request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByEmailAsync(request.Email);
            if (user == null)
                throw new UserNotFoundException(ErrorMessages.UserNotFound);

            var isPasswordValid = _passwordHasher.VerifyPasswordHash(request.Password, user.PasswordHash);
            if (!isPasswordValid)
                throw new InvalidCredentialsException(ErrorMessages.IncorrectPassword);

            if (!user.IsActive)
                throw new AccountNotActiveException(ErrorMessages.UserNotActive);

            if (!user.IsEmailConfirmed)
                throw new EmailNotConfirmedException(ErrorMessages.EmailNotConfirmed);

            var newAccess = await _jwtService.GenerateAccessTokenAsync(user.Id, user.Email, user.Role);
            var newRefresh = await _jwtService.GenerateRefreshTokenAsync();

            var refreshEntity = RefreshToken.Create(user.Id, newRefresh, DateTime.UtcNow.AddDays(30));
            await _refreshTokenRepository.AddAsync(refreshEntity, cancellationToken);

            return new AuthResultDto
            {
                Token = newAccess,
                RefreshToken = newRefresh,
                User = new UserDto
                {
                    Id = user.Id,
                    Email = user.Email,
                    IsActive = user.IsActive,
                    IsDeleted = user.IsDeleted,
                    IsEmailConfirmed = user.IsEmailConfirmed,
                    Role = user.Role
                }
            };
        }
    }
}
