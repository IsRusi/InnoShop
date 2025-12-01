using InnoShop.UserManager.Application.Common.Constants;
using InnoShop.UserManager.Application.Interfaces.IRepository;
using InnoShop.UserManager.Application.Interfaces.IService;
using InnoShop.UserManager.Domain.Exceptions;
using MediatR;

namespace InnoShop.UserManager.Application.Authentication.Commands.ResetPassword
{
    public class ResetPasswordCommandHandler : IRequestHandler<ResetPasswordCommand>
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IPasswordResetTokenRepository _passwordResetTokenRepository;

        public ResetPasswordCommandHandler(
            IPasswordHasher passwordHasher,
            IUserRepository userRepository,
            IPasswordResetTokenRepository passwordResetTokenRepository)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            _passwordResetTokenRepository = passwordResetTokenRepository;
        }

        public async Task Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
        {
            var resetToken = await _passwordResetTokenRepository.GetByTokenAsync(request.Token, cancellationToken);
            if (resetToken == null || !resetToken.IsValid())
                throw new InvalidTokenException(ErrorMessages.IncorrectToken);

            var user = await _userRepository.GetByIdAsync(request.UserId, cancellationToken);
            if (user is null)
                throw new UserNotFoundException(ErrorMessages.UserNotFound);

            if (resetToken.UserId != user.Id)
                throw new InvalidTokenException(ErrorMessages.IncorrectToken);

            user.ResetPassword(_passwordHasher.PasswordHash(request.NewPassword));
            await _userRepository.UpdateAsync(user, cancellationToken);

            resetToken.MarkAsUsed();
            await _passwordResetTokenRepository.UpdateAsync(resetToken, cancellationToken);
        }
    }
}