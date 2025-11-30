using FluentValidation;
using InnoShop.ProductManagment.Application.Common;

namespace InnoShop.ProductManagment.Application.Products.Commands.UpdateProduct
{
    public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
    {
        public UpdateProductCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Product ID is required");

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage(ErrorMessages.NameRequired)
                .MinimumLength(3).WithMessage(ErrorMessages.NameTooShort)
                .MaximumLength(100).WithMessage(ErrorMessages.NameTooLong);

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage(ErrorMessages.DescriptionRequired)
                .MaximumLength(500).WithMessage(ErrorMessages.DescriptionTooLong);

            RuleFor(x => x.Price)
                .GreaterThan(0).WithMessage(ErrorMessages.InvalidPrice);
        }
    }
}
