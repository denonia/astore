using System.ComponentModel.DataAnnotations;

namespace Astore.WebApi.Auth.Contracts;

public class RegisterRequest
{
    [EmailAddress] public string Email { get; set; }

    public string Password { get; set; }
}