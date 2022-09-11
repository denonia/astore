using Astore.Domain;

namespace Astore.Application;

public interface IArticleService
{
    Task<bool> CreateArticleAsync(Article article);
    Task<ICollection<Article>> GetArticlesAsync();
    Task<Article?> GetArticleByIdAsync(Guid id);
    Task<bool> UpdateArticleAsync(Article article);
    Task<bool> DeleteArticleAsync(Guid id);
}