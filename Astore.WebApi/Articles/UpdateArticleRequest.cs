namespace Astore.WebApi.Articles;

public class UpdateArticleRequest
{
    public string Name { get; set; }
    public decimal Price { get; set; }
    public string Category { get; set; }
}