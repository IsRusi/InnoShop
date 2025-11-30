using InnoShop.UserManager.Application.Interfaces.IRepository;
using InnoShop.UserManager.Application.Users.Commands.ActivateUser;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InnoShop.UserManager.Domain.Exceptions;
using InnoShop.UserManager.Application.Common.Constants;

namespace InnoShop.UserManager.Application.Users.Commands.DeactivateUser
{
        public class DeactivateUserCommandHandler : IRequestHandler<DeactivateUserCommand>
        {

            private readonly IUserRepository _usersRepository;
            public DeactivateUserCommandHandler(IUserRepository usersRepository)
            {
                _usersRepository = usersRepository;
            }

            public async Task Handle(DeactivateUserCommand request, CancellationToken cancellationToken)
            {
                var user = await _usersRepository.GetByIdAsync(request.userId);

                if (user is null)
                {
                throw new UserNotFoundException(ErrorMessages.UserIdIsRequired);
                }

                user.DeActivate();

                await _usersRepository.UpdateAsync(user);

            }
        }

}
