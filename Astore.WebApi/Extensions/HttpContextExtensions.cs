namespace Astore.WebApi.Extensions;

public static class HttpContextExtensions
{
    public static Guid GetUserId(this HttpContext context)
    {
        var idString = context.User.Claims.SingleOrDefault(x => x.Type == "id")?.Value;
        return Guid.Parse(idString);
    }
}