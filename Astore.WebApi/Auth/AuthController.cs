using Astore.WebApi.Auth.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace Astore.WebApi.Auth;

[Route("/auth")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(new AuthFailedResponse
            {
                Errors = ModelState.Values.SelectMany(entry => entry.Errors.Select(err => err.ErrorMessage))
            });

        var authResponse = await _authService.RegisterAsync(request.Email, request.Password);

        if (!authResponse.Success)
            return BadRequest(new AuthFailedResponse
            {
                Errors = authResponse.Errors
            });

        return Ok(new AuthSuccessResponse
        {
            Token = authResponse.Token
        });
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        var authResponse = await _authService.LoginAsync(request.Email, request.Password);

        if (!authResponse.Success)
            return BadRequest(new AuthFailedResponse
            {
                Errors = authResponse.Errors
            });

        return Ok(new AuthSuccessResponse
        {
            Token = authResponse.Token
        });
    }
}