using FluentValidation;
using InnoShop.UserManager.Application.Common.Constants;

namespace InnoShop.UserManager.Application.Authentication.Commands.Registration
{
    public class RegistrationCommandValidator : AbstractValidator<RegistrationCommand>
    {
        public RegistrationCommandValidator()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty()
                .WithMessage(ErrorMessages.NameIsRequired)
                .MaximumLength(100)
                .WithMessage(string.Format(ErrorMessages.NameMaxLengthExceeded, 100));

            RuleFor(x => x.SecondName)
                .NotEmpty()
                .WithMessage(ErrorMessages.NameIsRequired)
                .MaximumLength(100)
                .WithMessage(string.Format(ErrorMessages.NameMaxLengthExceeded, 100));

            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage(ErrorMessages.EmailIsRequired)
                .EmailAddress()
                .WithMessage(ErrorMessages.IncorrectEmailFormat);

            RuleFor(x => x.Password)
                .NotEmpty()
                .WithMessage(ErrorMessages.PasswordIsRequired)
                .MinimumLength(8)
                .WithMessage(string.Format(ErrorMessages.PasswordMustBeAtLeast, 8))
                .Matches(@"[A-Z]")
                .WithMessage(ErrorMessages.PasswordMustContainUpper)
                .Matches(@"[a-z]")
                .WithMessage(ErrorMessages.PasswordMustContainLower)
                .Matches(@"[0-9]")
                .WithMessage(ErrorMessages.PasswordMustContainDigit);
        }
    }
}
