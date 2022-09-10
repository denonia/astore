namespace Astore.WebApi.Articles;

public class UpdateArticleRequest
{
    public string Name { get; set; }
    public decimal Price { get; set; }
    public Guid CategoryId { get; set; }
}