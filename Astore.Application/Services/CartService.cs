using System.Collections.ObjectModel;
using Astore.Domain;
using Astore.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;

namespace Astore.Application.Services;

public class CartService : ICartService
{
    private readonly StoreDbContext _dbContext;

    public CartService(StoreDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    private async Task<UserProfile?> GetUserWithCartItemsAsync(Guid userId)
    {
        return await _dbContext.UserProfiles
            .Include(profile => profile.CartItems)
            .ThenInclude(item => item.Article)
            .SingleOrDefaultAsync(profile => profile.UserId == userId);
    }

    public async Task<ICollection<CartItem>> GetCartItemsAsync(Guid userId)
    {
        var user = await GetUserWithCartItemsAsync(userId);
        if (user == null)
            return (ICollection<CartItem>)Enumerable.Empty<CartItem>();

        return user.CartItems;
    }

    public async Task<bool> UpdateCartAsync(Guid userId, ICollection<CartItem> cart)
    {
        var user = await GetUserWithCartItemsAsync(userId);
        if (user == null)
            return false;

        user.CartItems = cart;
        _dbContext.UserProfiles.Update(user);
        return await _dbContext.SaveChangesAsync() > 0;
    }

    public async Task<bool> ClearCartAsync(Guid userId)
    {
        var user = await GetUserWithCartItemsAsync(userId);
        if (user == null)
            return false;
        
        user.CartItems.Clear();
        return await _dbContext.SaveChangesAsync() > 0;
    }

    public async Task<bool> DeleteArticleFromCartAsync(Guid userId, Guid articleId)
    {
        var user = await GetUserWithCartItemsAsync(userId);
        if (user == null)
            return false;
        
        foreach (var item in user.CartItems.Where(item => item.Article.Id == articleId))
        {
            user.CartItems.Remove(item);
        }
        return await _dbContext.SaveChangesAsync() > 0;
    }
}