namespace Astore.WebApi.Articles;

public class GetAllArticlesResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public string Category { get; set; }
}