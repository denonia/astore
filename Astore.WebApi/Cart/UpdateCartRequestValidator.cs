using FluentValidation;

namespace Astore.WebApi.Cart;

public class UpdateCartRequestValidator : AbstractValidator<UpdateCartRequest>
{
    public UpdateCartRequestValidator()
    {
        RuleFor(request => request.Quantity).NotEmpty().InclusiveBetween(0, 20);
        RuleFor(request => request.ArticleId).NotEmpty();
    }
}