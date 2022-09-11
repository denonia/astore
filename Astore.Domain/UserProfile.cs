namespace Astore.Domain;

public class UserProfile
{
    public Guid UserId { get; set; }
    public string? Address { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public ICollection<Review> Reviews { get; set; }
    public ICollection<Article> Favorites { get; set; }
    public ICollection<CartItem> CartItems { get; set; }
}