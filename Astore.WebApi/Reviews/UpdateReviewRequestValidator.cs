using FluentValidation;

namespace Astore.WebApi.Reviews;

public class UpdateReviewRequestValidator : AbstractValidator<CreateReviewRequest>
{
    public UpdateReviewRequestValidator()
    {
        RuleFor(r => r.Body).NotEmpty().MaximumLength(500);
        RuleFor(r => r.Rating).InclusiveBetween(0, 10);
    }
}