using FluentValidation;
using InnoShop.ProductManagment.Application.DTOs;
using InnoShop.ProductManagment.Domain.Common;

namespace InnoShop.ProductManagment.Application.Products.Queries.SearchProducts
{
    public class SearchProductsRequestValidator : AbstractValidator<SearchProductsRequest>
    {
        public SearchProductsRequestValidator()
        {
            RuleFor(x => x.SearchTerm)
                .MaximumLength(500)
                .WithMessage(ErrorMessages.SearchTermTooLong);

            RuleFor(x => x.MinPrice)
                .GreaterThanOrEqualTo(0)
                .WithMessage(ErrorMessages.MinPriceNegative)
                .When(x => x.MinPrice.HasValue);

            RuleFor(x => x.MaxPrice)
                .GreaterThanOrEqualTo(0)
                .WithMessage(ErrorMessages.MaxPriceNegative)
                .When(x => x.MaxPrice.HasValue);

            RuleFor(x => x)
                .Must(x => !x.MinPrice.HasValue || !x.MaxPrice.HasValue || x.MinPrice <= x.MaxPrice)
                .WithMessage(ErrorMessages.PriceRangeInvalid);

            RuleFor(x => x.UserId)
                .NotEqual(Guid.Empty)
                .WithMessage(ErrorMessages.UserIdEmpty)
                .When(x => x.UserId.HasValue && x.UserId != Guid.Empty);
        }
    }
}
