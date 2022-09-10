namespace Astore.WebApi.Articles;

public class GetArticleResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public string Category { get; set; }
    public ICollection<GetArticleResponseReview> Reviews { get; set; }
}

public class GetArticleResponseReview
{
    public Guid AuthorId { get; set; }
    public string Body { get; set; }
    public int Rating { get; set; }
}