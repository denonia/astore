using Astore.Domain;

namespace Astore.Application;

public interface IFavoritesService
{
    Task<ICollection<Article>> GetUserFavoritesAsync(Guid userId);
    Task<bool> MoveFavoritesToCartAsync(Guid userId);
    Task<bool> UpdateFavoritesAsync(Guid userId, ICollection<Guid> articleIds);
    Task<bool> ClearFavoritesAsync(Guid userId);
    Task<bool> DeleteArticleFromFavoritesAsync(Guid userId, Guid articleId);
}