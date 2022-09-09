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
    
    private async Task<User?> GetUserWithFavoritesAsync(Guid userId)
    {
        return await _dbContext.Users
            .Include(user => user.Profile.Favorites)
            .SingleOrDefaultAsync(user => user.Id == userId);
    }
    
    private async Task<User?> GetUserWithFavoritesAndCartItemsAsync(Guid userId)
    {
        return await _dbContext.Users
            .Include(user => user.Profile.Favorites)
            .Include(user => user.Profile.CartItems)
            .SingleOrDefaultAsync(user => user.Id == userId);
    }
    
    public async Task<ICollection<Article>> GetUserFavoritesAsync(Guid userId)
    {
        var user = await GetUserWithFavoritesAsync(userId);
        if (user == null)
            return (ICollection<Article>)Enumerable.Empty<Article>();

        return user.Profile.Favorites;
    }

    public async Task<bool> MoveFavoritesToCartAsync(Guid userId)
    {
        var user = await GetUserWithFavoritesAndCartItemsAsync(userId);
        if (user == null)
            return false;
        
        foreach (var favoriteArticle in user.Profile.Favorites)
        {
            var cartArticle = user.Profile.CartItems.SingleOrDefault(item => item.Article == favoriteArticle);

            if (cartArticle != null)
                cartArticle.Quantity++;
            
            user.Profile.CartItems.Add(new CartItem
            {
                Article = favoriteArticle,
                Quantity = 1
            });
        }

        return await _dbContext.SaveChangesAsync() > 0;
    }

    public async Task<bool> UpdateFavoritesAsync(Guid userId, ICollection<Article> items)
    {
        var user = await GetUserWithFavoritesAsync(userId);
        if (user == null)
            return false;

        user.Profile.Favorites = items;
        return await _dbContext.SaveChangesAsync() > 0;
    }

    public async Task<bool> ClearFavoritesAsync(Guid userId)
    {
        var user = await GetUserWithFavoritesAsync(userId);
        if (user == null)
            return false;
        
        user.Profile.Favorites.Clear();
        return await _dbContext.SaveChangesAsync() > 0;
    }

    public async Task<bool> DeleteArticleFromFavoritesAsync(Guid userId, Article article)
    {
        var user = await GetUserWithFavoritesAsync(userId);
        if (user == null)
            return false;

        user.Profile.Favorites.Remove(article);
        return await _dbContext.SaveChangesAsync() > 0;
    }
}