using InnoShop.UserManager.Application.Interfaces.IRepository;
using InnoShop.UserManager.Application.Common.Constants;
using InnoShop.UserManager.Domain.Exceptions;
using MediatR;

namespace InnoShop.UserManager.Application.Authentication.Commands.Logout
{
    public class LogoutCommandHandler(IRefreshTokenRepository refreshTokenRepository) : IRequestHandler<LogoutCommand>
    {
        public async Task Handle(LogoutCommand request, CancellationToken cancellationToken)
        {
            var refreshToken = await refreshTokenRepository.GetByTokenAsync(request.RefreshToken, cancellationToken);
            
            if (refreshToken == null)
            {
                throw new TokenNotFoundException(ErrorMessages.TokenNotFound);
            }

            if (refreshToken.UserId != request.UserId)
            {
                throw new TokenNotFoundException(ErrorMessages.TokenNotFound);
            }

            refreshToken.Revoke();
            await refreshTokenRepository.UpdateAsync(refreshToken, cancellationToken);
        }
    }
}
