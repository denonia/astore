namespace Astore.WebApi.Articles;

public class CreateArticleRequest
{
    public string Name { get; set; }
    public decimal Price { get; set; }
    public Guid CategoryId { get; set; }
}