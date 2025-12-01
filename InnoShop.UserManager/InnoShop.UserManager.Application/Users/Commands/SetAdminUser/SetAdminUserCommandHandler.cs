using InnoShop.UserManager.Application.Common.Constants;
using InnoShop.UserManager.Application.Interfaces.IRepository;
using InnoShop.UserManager.Application.Users.Commands.DeactivateUser;
using InnoShop.UserManager.Domain.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InnoShop.UserManager.Application.Users.Commands.SetAdminUser
{
    public class SetAdminUserCommandHandler:IRequestHandler<SetAdminUserCommand>
    {
        private readonly IUserRepository _usersRepository;

        public SetAdminUserCommandHandler(IUserRepository usersRepository)
        {
            _usersRepository = usersRepository;
        }

        public async Task Handle(SetAdminUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _usersRepository.GetByIdAsync(request.userId);

            if (user is null)
            {
                throw new UserNotFoundException(ErrorMessages.UserNotFound);
            }

            user.SetAdmin();

            await _usersRepository.UpdateAsync(user);
        }
    }
}
