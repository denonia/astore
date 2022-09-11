namespace Astore.Domain;

public class CartItem
{
    public Guid Id { get; set; }
    public UserProfile UserProfile { get; set; }
    public Article Article { get; set; }
    public int Quantity { get; set; }
}