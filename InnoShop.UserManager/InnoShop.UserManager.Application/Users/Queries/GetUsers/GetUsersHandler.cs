using InnoShop.UserManager.Application.Interfaces.IRepository;
using InnoShop.UserManager.Domain.Models;
using MediatR;
using InnoShop.UserManager.Application.DTOs;

namespace InnoShop.UserManager.Application.Users.Queries.GetUsers
{
    public class GetUsersHandler:IRequestHandler<GetUserQuery,UserDto>
    {
        private readonly IUserRepository _userRepository;
        public GetUsersHandler(IUserRepository userRepository) => _userRepository = userRepository;
        public async Task<UserDto> Handle(GetUserQuery request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(request.id);
            var userDtos = new UserDto() 
            {
                Id=user.Id, 
                Email = user.Email,
                IsActive=user.IsActive,
                IsDeleted=user.IsDeleted,
                IsEmailConfirmed=user.IsEmailConfirmed,
                Role=user.Role,
                PasswordHash=user.PasswordHash
    };
            return userDtos;
        }
    }
}
