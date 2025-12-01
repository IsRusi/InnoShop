using InnoShop.UserManager.Application.Common.Constants;
using InnoShop.UserManager.Application.Interfaces.IRepository;
using InnoShop.UserManager.Domain.Exceptions;
using MediatR;

namespace InnoShop.UserManager.Application.Users.Commands.ActivateUser
{
    public class ActivateUserHandler : IRequestHandler<ActivateUserCommand>
    {
        private readonly IUserRepository _usersRepository;

        public ActivateUserHandler(IUserRepository usersRepository)
        {
            _usersRepository = usersRepository;
        }

        public async Task Handle(ActivateUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _usersRepository.GetByIdAsync(request.userId);

            if (user is null)
            {
                throw new UserNotFoundException(ErrorMessages.UserNotFound);
            }

            user.Activate();

            await _usersRepository.UpdateAsync(user);
        }
    }
}