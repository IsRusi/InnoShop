using FluentValidation;
using InnoShop.UserManager.Application.Common.Constants;

namespace InnoShop.UserManager.Application.Authentication.Commands.ConfirmEmail
{
    public class ConfirmEmailCommandValidator : AbstractValidator<ConfirmEmailCommand>
    {
        public ConfirmEmailCommandValidator()
        {
            RuleFor(x => x.userId)
                .NotEmpty()
                .WithMessage(ErrorMessages.UserIdIsRequired);

            RuleFor(x => x.token)
                .NotEmpty()
                .WithMessage(ErrorMessages.ConfirmationTokenIsRequired);
        }
    }
}