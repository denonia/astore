namespace Astore.WebApi.Users;

public class GetUserResponse
{
    public Guid UserId { get; set; }
    public string? Address { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public ICollection<GetUserResponseReview> Reviews { get; set; }
    public ICollection<GetUserResponseArticle> Favorites { get; set; }
    public ICollection<GetUserResponseCartItem> CartItems { get; set; }
}

public class GetUserResponseReview
{
    public Guid Id { get; set; }
    public string Body { get; set; }
    public int Rating { get; set; }
    public Guid ArticleId { get; set; }
}

public class GetUserResponseArticle
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public string Category { get; set; }
}

public class GetUserResponseCartItem
{
    public GetUserResponseArticle Article { get; set; }
    public int Quantity { get; set; }
}