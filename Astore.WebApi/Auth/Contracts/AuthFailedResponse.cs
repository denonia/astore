namespace Astore.WebApi.Auth.Contracts;

public class AuthFailedResponse
{
    public IEnumerable<string> Errors { get; set; }
}