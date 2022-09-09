namespace Astore.WebApi.Extensions;

public static class HttpContextExtensions
{
    public static string? GetUserId(this HttpContext context)
    {
        return context.User.Claims.SingleOrDefault(x => x.Type == "id")?.Value;
    }
}