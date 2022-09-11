using FluentValidation;

namespace Astore.WebApi.Articles;

public class CreateArticleRequestValidator : AbstractValidator<CreateArticleRequest>
{
    public CreateArticleRequestValidator()
    {
        RuleFor(x => x.Name).NotEmpty().MaximumLength(250);
        RuleFor(x => x.Price).NotEmpty();
        RuleFor(x => x.CategoryId).NotEmpty();
    }
}