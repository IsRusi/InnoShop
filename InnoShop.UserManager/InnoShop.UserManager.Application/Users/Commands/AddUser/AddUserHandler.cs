using InnoShop.UserManager.Application.DTOs;
using InnoShop.UserManager.Application.Interfaces.IRepository;
using InnoShop.UserManager.Domain.Models;
using MediatR;
using InnoShop.UserManager.Domain.Exceptions;
using InnoShop.UserManager.Application.Common.Constants;

namespace InnoShop.UserManager.Application.Users.Commands.AddUser
{
    public class AddUserHandler : IRequestHandler<AddUserCommand>
    {
        private readonly IUserRepository _userRepository;
        public AddUserHandler(IUserRepository userRepository) => _userRepository = userRepository;

        public async Task Handle(AddUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByEmailAsync(request.userDto.Email);
            if(user != null)
            {
                throw new UserNotFoundException(ErrorMessages.EmailAlreadyExists);
            }
            var newUser = new User()
            {
                Id = request.userDto.Id,
                Email = request.userDto.Email,
                IsActive = request.userDto.IsActive,
                IsDeleted = request.userDto.IsDeleted,
                IsEmailConfirmed = request.userDto.IsEmailConfirmed,
                Role = request.userDto.Role,
                PasswordHash = request.userDto.PasswordHash
            };

            await _userRepository.AddAsync(newUser);
        }
    }
}
