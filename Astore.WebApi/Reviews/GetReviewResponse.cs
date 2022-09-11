namespace Astore.WebApi.Reviews;

public class GetReviewResponse
{
    public Guid Id { get; set; }
    public Guid AuthorId { get; set; }
    public string Body { get; set; }
    public int Rating { get; set; }
}