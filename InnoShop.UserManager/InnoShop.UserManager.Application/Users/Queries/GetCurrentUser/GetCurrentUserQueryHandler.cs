using InnoShop.Shared.Application.Interfaces;
using InnoShop.UserManager.Application.DTOs;
using InnoShop.UserManager.Application.Interfaces.IRepository;
using MediatR;

namespace InnoShop.UserManager.Application.Users.Queries.GetCurrentUser
{
    public class GetCurrentUserQueryHandler(IUserRepository _userRepository, ICurrentUserService currentUserService)
      : IRequestHandler<GetCurrentUserQuery, UserDto>
    {
        public async Task<UserDto> Handle(GetCurrentUserQuery request, CancellationToken cancellationToken = default)
        {
            var userId = currentUserService.UserId ?? throw new UnauthorizedAccessException();
            var user = await _userRepository.GetByIdAsync(userId,cancellationToken);
            return new UserDto()
            {
            Id= user.Id,
            FirstName= user.FirstName,
            SecondName= user.LastName,
            Email= user.Email,
            Role= user.Role,
            IsEmailConfirmed= user.IsEmailConfirmed,
            IsActive= user.IsActive,
            IsDeleted= user.IsDeleted,
        } ;
        }
    }
}
