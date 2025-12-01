using InnoShop.UserManager.Application.Common.Constants;
using InnoShop.UserManager.Application.Interfaces.IRepository;
using InnoShop.UserManager.Domain.Exceptions;
using MediatR;

namespace InnoShop.UserManager.Application.Authentication.Commands.ConfirmEmail
{
    public class ConfirmEmailCommandHandler : IRequestHandler<ConfirmEmailCommand>
    {
        private readonly IUserRepository _userRepository;
        private readonly IEmailConfirmationTokenRepository _emailConfirmationTokenRepository;

        public ConfirmEmailCommandHandler(
            IUserRepository userRepository,
            IEmailConfirmationTokenRepository emailConfirmationTokenRepository)
        {
            _userRepository = userRepository;
            _emailConfirmationTokenRepository = emailConfirmationTokenRepository;
        }

        public async Task Handle(ConfirmEmailCommand request, CancellationToken cancellationToken)
        {
            var confirmationToken = await _emailConfirmationTokenRepository.GetByTokenAsync(request.token, cancellationToken);
            if (confirmationToken == null || !confirmationToken.IsValid())
                throw new InvalidTokenException(ErrorMessages.IncorrectToken);

            var user = await _userRepository.GetByIdAsync(request.userId, cancellationToken);
            if (user == null)
                throw new UserNotFoundException(ErrorMessages.UserNotFound);

            if (confirmationToken.UserId != user.Id)
                throw new InvalidTokenException(ErrorMessages.IncorrectToken);

            user.VerifyEmail();
            await _userRepository.UpdateAsync(user, cancellationToken);

            confirmationToken.MarkAsUsed();
            await _emailConfirmationTokenRepository.UpdateAsync(confirmationToken, cancellationToken);
        }
    }
}