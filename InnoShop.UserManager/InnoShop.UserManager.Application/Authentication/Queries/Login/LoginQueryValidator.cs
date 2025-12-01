using FluentValidation;
using InnoShop.UserManager.Application.Common.Constants;

namespace InnoShop.UserManager.Application.Authentication.Queries.Login
{
    public class LoginQueryValidator : AbstractValidator<LoginQuery>
    {
        public LoginQueryValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage(ErrorMessages.EmailIsRequired)
                .EmailAddress()
                .WithMessage(ErrorMessages.IncorrectEmailFormat);

            RuleFor(x => x.Password)
                .NotEmpty()
                .WithMessage(ErrorMessages.PasswordIsRequired)
                .MinimumLength(8)
                .WithMessage(string.Format(ErrorMessages.PasswordMustBeAtLeast, 8));
        }
    }
}