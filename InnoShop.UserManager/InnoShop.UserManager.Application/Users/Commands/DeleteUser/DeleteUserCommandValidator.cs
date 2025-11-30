using FluentValidation;
using InnoShop.UserManager.Application.Common.Constants;

namespace InnoShop.UserManager.Application.Users.Commands.DeleteUser
{
    public class DeleteUserCommandValidator : AbstractValidator<DeleteUserCommand>
    {
        public DeleteUserCommandValidator()
        {
            RuleFor(x => x.userId)
                .NotEmpty()
                .WithMessage(ErrorMessages.UserIdIsRequired);
        }
    }
}
