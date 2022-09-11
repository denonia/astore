using FluentValidation;

namespace Astore.WebApi.Articles;

public class UpdateArticleRequestValidator : AbstractValidator<UpdateArticleRequest>
{
    public UpdateArticleRequestValidator()
    {
        RuleFor(x => x.Name).NotEmpty().MaximumLength(250);
        RuleFor(x => x.Price).NotEmpty();
        RuleFor(x => x.CategoryId).NotEmpty();
    }
}