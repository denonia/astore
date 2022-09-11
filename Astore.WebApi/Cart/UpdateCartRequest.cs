namespace Astore.WebApi.Cart;

public class UpdateCartRequest
{
    public Guid ArticleId { get; set; }
    public int Quantity { get; set; }
}