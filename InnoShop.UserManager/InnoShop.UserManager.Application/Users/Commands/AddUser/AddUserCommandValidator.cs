using FluentValidation;
using InnoShop.UserManager.Application.Common.Constants;

namespace InnoShop.UserManager.Application.Users.Commands.AddUser
{
    public class AddUserCommandValidator : AbstractValidator<AddUserCommand>
    {
        public AddUserCommandValidator()
        {
            RuleFor(x => x.userDto)
                .NotNull()
                .WithMessage(ErrorMessages.UserDataIsRequired);

            When(x => x.userDto != null, () =>
            {
                RuleFor(x => x.userDto.Email)
                    .NotEmpty()
                    .WithMessage(ErrorMessages.EmailIsRequired)
                    .EmailAddress()
                    .WithMessage(ErrorMessages.IncorrectEmailFormat);

                RuleFor(x => x.userDto.Role)
                    .NotEmpty()
                    .WithMessage(ErrorMessages.RoleIsRequired);
            });
        }
    }
}