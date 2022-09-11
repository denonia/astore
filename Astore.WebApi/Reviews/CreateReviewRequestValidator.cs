using FluentValidation;

namespace Astore.WebApi.Reviews;

public class CreateReviewRequestValidator : AbstractValidator<CreateReviewRequest>
{
    public CreateReviewRequestValidator()
    {
        RuleFor(r => r.Body).NotEmpty().MaximumLength(500);
        RuleFor(r => r.Rating).InclusiveBetween(0, 10);
    }
}