using FluentValidation;

namespace SecondHandBook.Models.Validators
{
    public class SearchQueryValidator : AbstractValidator<SearchQuery>
    {
        private int[] allowedSizes = new[] { 6, 12, 18 };
        public SearchQueryValidator()
        {
            RuleFor(x => x.PageNumber).GreaterThanOrEqualTo(1);
            RuleFor(x => x.PageSize).Custom((value, context) =>
            {
                if (!allowedSizes.Contains(value))
                {
                    context.AddFailure("PageSize", $"PageSize must be in range [{string.Join(",", allowedSizes)}]");
                }
            });
        }
    }
}
