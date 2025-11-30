using FluentValidation;
using InnoShop.UserManager.Application.Common.Constants;

namespace InnoShop.UserManager.Application.Authentication.Commands.ResetPassword
{
    public class ResetPasswordCommandValidator : AbstractValidator<ResetPasswordCommand>
    {
        public ResetPasswordCommandValidator()
        {
            RuleFor(x => x.UserId)
                .NotEmpty()
                .WithMessage(ErrorMessages.UserIdIsRequired);

            RuleFor(x => x.NewPassword)
                .NotEmpty()
                .WithMessage(ErrorMessages.PasswordIsRequired)
                .MinimumLength(8)
                .WithMessage(string.Format(ErrorMessages.PasswordMustBeAtLeast, 8));
        }
    }
}
