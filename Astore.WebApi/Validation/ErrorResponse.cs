namespace Astore.WebApi.Validation;

public class ErrorResponse
{
    public List<ErrorModel> Errors { get; set; } = new();
}