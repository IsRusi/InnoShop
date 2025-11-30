using InnoShop.UserManager.Application.Interfaces.IRepository;
using InnoShop.UserManager.Application.Interfaces.IService;
using InnoShop.UserManager.Domain.Models;
using MediatR;
using InnoShop.UserManager.Domain.Exceptions;
using InnoShop.UserManager.Application.Common.Constants;

namespace InnoShop.UserManager.Application.Authentication.Commands.ResetPassword
{
    public class ResetPasswordCommandHandler : IRequestHandler<ResetPasswordCommand>
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher _passwordHasher;

        public ResetPasswordCommandHandler(IPasswordHasher passwordHasher, IUserRepository userRepository)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
        }

        public async Task Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(request.UserId);
            if (user is null) throw new UserNotFoundException(ErrorMessages.UserNotFound);

            user.ResetPassword(_passwordHasher.PasswordHash(request.NewPassword));
            await _userRepository.UpdateAsync(user);
        }
    }
}
