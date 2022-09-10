namespace Astore.WebApi.Users;

public class UpdateUserRequest
{
    public string? Email { get; set; }
    public string? Address { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
}