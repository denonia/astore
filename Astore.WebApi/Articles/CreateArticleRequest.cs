namespace Astore.WebApi.Articles;

public class CreateArticleRequest
{
    public string Name { get; set; }
    public decimal Price { get; set; }
    public string Category { get; set; }
}