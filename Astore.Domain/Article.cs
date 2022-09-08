namespace Astore.Domain
{
    public class Article
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public Category Category { get; set; }
        public ICollection<Review> Reviews { get; set; }
        public ICollection<UserProfile> FavoritedBy { get; set; }
        public ICollection<CartItem> CartItems { get; set; }
    }
}