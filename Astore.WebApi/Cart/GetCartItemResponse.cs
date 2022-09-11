namespace Astore.WebApi.Cart;

public class GetCartItemResponse
{
    public Guid ArticleId { get; set; }
    public int Quantity { get; set; }
}