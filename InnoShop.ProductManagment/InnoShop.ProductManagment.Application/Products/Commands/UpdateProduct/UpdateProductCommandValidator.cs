using FluentValidation;
using InnoShop.ProductManagment.Application.Common.Constants;

namespace InnoShop.ProductManagment.Application.Products.Commands.UpdateProduct
{
    public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
    {
        public UpdateProductCommandValidator()
        {
            RuleFor(x => x.id)
                .NotEmpty().WithMessage(ErrorMessages.AlreadyProductExists);

            RuleFor(x => x.name)
                .NotEmpty().WithMessage(ErrorMessages.NameRequired)
                .MinimumLength(3).WithMessage(ErrorMessages.NameTooShort)
                .MaximumLength(100).WithMessage(ErrorMessages.NameTooLong);

            RuleFor(x => x.description)
                .NotEmpty().WithMessage(ErrorMessages.DescriptionRequired)
                .MaximumLength(500).WithMessage(ErrorMessages.DescriptionTooLong);

            RuleFor(x => x.price)
                .GreaterThan(0).WithMessage(ErrorMessages.InvalidPrice);
        }
    }
}
