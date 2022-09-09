using Astore.Domain;

namespace Astore.Application;

public interface IFavoritesService
{
    Task<ICollection<Article>> GetUserFavoritesAsync(Guid userId);
    Task<bool> MoveFavoritesToCartAsync(Guid userId);
    Task<bool> UpdateFavoritesAsync(Guid userId, ICollection<Article> items);
    Task<bool> ClearFavoritesAsync(Guid userId);
    Task<bool> DeleteArticleFromFavoritesAsync(Guid userId, Article article);
}