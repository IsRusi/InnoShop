using InnoShop.UserManager.Application.Common.Constants;
using InnoShop.UserManager.Application.Interfaces.IRepository;
using InnoShop.UserManager.Domain.Exceptions;
using MediatR;

namespace InnoShop.UserManager.Application.Users.Commands.UpdateUser
{
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand>
    {
        private readonly IUserRepository _userRepository;

        public UpdateUserCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(request.UserId);

            if (user is null)
            {
                throw new UserNotFoundException(ErrorMessages.UserNotFound);
            }

            user.FirstName = request.FirstName;
            user.LastName = request.LastName;
            user.Email = request.Email;

            await _userRepository.UpdateAsync(user);
        }
    }
}