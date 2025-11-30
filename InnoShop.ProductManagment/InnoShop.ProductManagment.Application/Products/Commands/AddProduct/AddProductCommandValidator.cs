using FluentValidation;
using InnoShop.ProductManagment.Application.Common.Constants;

namespace InnoShop.ProductManagment.Application.Products.Commands.AddProduct
{
    public class AddProductCommandValidator : AbstractValidator<AddProductCommand>
    {
        public AddProductCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage(ErrorMessages.NameRequired)
                .MinimumLength(3).WithMessage(ErrorMessages.NameTooShort)
                .MaximumLength(100).WithMessage(ErrorMessages.NameTooLong);

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage(ErrorMessages.DescriptionRequired)
                .MaximumLength(500).WithMessage(ErrorMessages.DescriptionTooLong);

            RuleFor(x => x.Price)
                .GreaterThan(0).WithMessage(ErrorMessages.InvalidPrice);

            RuleFor(x => x.UserId)
                .NotEmpty().WithMessage(ErrorMessages.UserIsNull);
        }
    }
}
