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

    private async Task<User?> GetUserWithCartItemsAsync(Guid userId)
    {
        return await _dbContext.Users
            .Include(user => user.Profile.CartItems)
            .SingleOrDefaultAsync(user => user.Id == userId);
    }

    public async Task<ICollection<CartItem>> GetCartItemsAsync(Guid userId)
    {
        var user = await GetUserWithCartItemsAsync(userId);
        if (user == null)
            return (ICollection<CartItem>)Enumerable.Empty<CartItem>();

        return user.Profile.CartItems;
    }

    public async Task<bool> UpdateCartAsync(Guid userId, ICollection<CartItem> cart)
    {
        var user = await GetUserWithCartItemsAsync(userId);
        if (user == null)
            return false;

        user.Profile.CartItems = cart;
        _dbContext.UserProfiles.Update(user.Profile);
        return await _dbContext.SaveChangesAsync() > 0;
    }

    public async Task<bool> ClearCartAsync(Guid userId)
    {
        var user = await GetUserWithCartItemsAsync(userId);
        if (user == null)
            return false;
        
        user.Profile.CartItems.Clear();
        return await _dbContext.SaveChangesAsync() > 0;
    }

    public async Task<bool> DeleteArticleFromCartAsync(Guid userId, Article article)
    {
        var user = await GetUserWithCartItemsAsync(userId);
        if (user == null)
            return false;
        
        foreach (var item in user.Profile.CartItems.Where(item => item.Article == article))
        {
            user.Profile.CartItems.Remove(item);
        }
        return await _dbContext.SaveChangesAsync() > 0;
    }
}