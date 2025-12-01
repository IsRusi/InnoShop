using InnoShop.UserManager.Application.DTOs;
using InnoShop.UserManager.Application.Interfaces.IRepository;
using MediatR;

namespace InnoShop.UserManager.Application.Users.Queries.GetUserById
{
    public class GetUserByIdQueryHandler(IUserRepository _userRepository)
       : IRequestHandler<GetUserByIdQuery, UserDto>
    {
        public async Task<UserDto> Handle(GetUserByIdQuery request, CancellationToken cancellationToken = default)
        {
            var user = await _userRepository.GetByIdAsync(request.id, cancellationToken);
            return new UserDto()
            {
                Id = user.Id,
                FirstName = user.FirstName,
                SecondName = user.LastName,
                Email = user.Email,
                Role = user.Role,
                IsEmailConfirmed = user.IsEmailConfirmed,
                IsActive = user.IsActive,
                IsDeleted = user.IsDeleted,
            };
        }
    }
}