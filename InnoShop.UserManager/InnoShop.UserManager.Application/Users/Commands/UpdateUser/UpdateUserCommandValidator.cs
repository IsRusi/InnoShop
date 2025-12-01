using FluentValidation;
using InnoShop.UserManager.Application.Common.Constants;

namespace InnoShop.UserManager.Application.Users.Commands.UpdateUser
{
    public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
    {
        public UpdateUserCommandValidator()
        {
            RuleFor(x => x.UserId)
                .NotEmpty().WithMessage(ErrorMessages.UserNotFound);

            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage(ErrorMessages.NameIsRequired)
                .MaximumLength(100).WithMessage(ErrorMessages.NameMaxLengthExceeded);

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage(ErrorMessages.NameIsRequired)
                .MaximumLength(100).WithMessage(ErrorMessages.NameMaxLengthExceeded);

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage(ErrorMessages.EmailIsRequired)
                .EmailAddress().WithMessage(ErrorMessages.EmailIsRequired);
        }
    }
}