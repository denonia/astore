using Astore.Domain;

namespace Astore.Application;

public interface ICartService
{
    Task<ICollection<CartItem>> GetCartItemsAsync(Guid userId);
    Task<bool> UpdateCartAsync(Guid userId, ICollection<CartItem> cart);
    Task<bool> ClearCartAsync(Guid userId);
    Task<bool> DeleteArticleFromCartAsync(Guid userId, Article article);
}