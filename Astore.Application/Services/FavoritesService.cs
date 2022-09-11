using Astore.Domain;
using Astore.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Astore.Application.Services;

public class FavoritesService : IFavoritesService
{
    private readonly StoreDbContext _dbContext;

    public FavoritesService(StoreDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<ICollection<Article>> GetUserFavoritesAsync(Guid userId)
    {
        var user = await GetUserWithFavoritesAsync(userId);
        if (user == null)
            return (ICollection<Article>)Enumerable.Empty<Article>();

        return user.Favorites;
    }

    public async Task<bool> MoveFavoritesToCartAsync(Guid userId)
    {
        var user = await GetUserWithFavoritesAndCartItemsAsync(userId);
        if (user == null)
            return false;

        foreach (var favoriteArticle in user.Favorites)
        {
            var cartArticle = user.CartItems.SingleOrDefault(item => item.Article == favoriteArticle);

            if (cartArticle != null)
                cartArticle.Quantity++;

            user.CartItems.Add(new CartItem
            {
                Article = favoriteArticle,
                Quantity = 1
            });
        }

        return await _dbContext.SaveChangesAsync() > 0;
    }

    public async Task<bool> UpdateFavoritesAsync(Guid userId, ICollection<Guid> articleIds)
    {
        var user = await GetUserWithFavoritesAsync(userId);
        if (user == null)
            return false;

        user.Favorites.Clear();
        foreach (var id in articleIds)
            user.Favorites.Add(await _dbContext.Articles.SingleOrDefaultAsync(article => article.Id == id));
        return await _dbContext.SaveChangesAsync() > 0;
    }

    public async Task<bool> ClearFavoritesAsync(Guid userId)
    {
        var user = await GetUserWithFavoritesAsync(userId);
        if (user == null)
            return false;

        user.Favorites.Clear();
        return await _dbContext.SaveChangesAsync() > 0;
    }

    public async Task<bool> DeleteArticleFromFavoritesAsync(Guid userId, Guid articleId)
    {
        var user = await GetUserWithFavoritesAsync(userId);
        if (user == null)
            return false;

        var article = user.Favorites.SingleOrDefault(article => article.Id == articleId);
        user.Favorites.Remove(article);
        return await _dbContext.SaveChangesAsync() > 0;
    }

    private async Task<UserProfile?> GetUserWithFavoritesAsync(Guid userId)
    {
        return await _dbContext.UserProfiles
            .Include(profile => profile.Favorites)
            .SingleOrDefaultAsync(profile => profile.UserId == userId);
    }

    private async Task<UserProfile?> GetUserWithFavoritesAndCartItemsAsync(Guid userId)
    {
        return await _dbContext.UserProfiles
            .Include(profile => profile.Favorites)
            .Include(profile => profile.CartItems)
            .SingleOrDefaultAsync(profile => profile.UserId == userId);
    }
}