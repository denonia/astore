using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Astore.Application;
using Astore.Domain;
using Astore.Persistence;
using Astore.WebApi.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace Astore.WebApi.Auth;

public class AuthService : IAuthService
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly StoreDbContext _dbContext;
    private readonly JwtSettings _jwtSettings;

    public AuthService(UserManager<IdentityUser> userManager, StoreDbContext dbContext, JwtSettings jwtSettings)
    {
        _userManager = userManager;
        _dbContext = dbContext;
        _jwtSettings = jwtSettings;
    }
    
    public async Task<AuthResult> RegisterAsync(string email, string password)
    {
        var existingUser = await _userManager.FindByEmailAsync(email);

        if (existingUser != null)
        {
            return new AuthResult
            {
                Errors = new []{"User with this email address already exists"}
            };
        }
        
        var newUser = new IdentityUser
        {
            Email = email,
            UserName = email
        };

        var createdUser = await _userManager.CreateAsync(newUser, password);

        if (!createdUser.Succeeded)
        {
            return new AuthResult
            {
                Errors = createdUser.Errors.Select(err => err.Description)
            };
        }

        await _dbContext.UserProfiles.AddAsync(new UserProfile { UserId = Guid.Parse(newUser.Id) });
        await _dbContext.SaveChangesAsync();

        return GenerateAuthResult(newUser);
    }

    public async Task<AuthResult> LoginAsync(string email, string password)
    {
        var user = await _userManager.FindByEmailAsync(email);

        if (user == null)
        {
            return new AuthResult { Errors = new []{"Invalid email or password"} };
        }

        var isPasswordValid = await _userManager.CheckPasswordAsync(user, password);
        
        if (!isPasswordValid)
        {
            return new AuthResult { Errors = new []{"Invalid email or password"} };
        }

        return GenerateAuthResult(user);
    }
    
    private AuthResult GenerateAuthResult(IdentityUser newUser)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_jwtSettings.Secret);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, newUser.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, newUser.Email),
                new Claim("id", newUser.Id)
            }),
            Expires = DateTime.UtcNow.AddHours(2),
            SigningCredentials =
                new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return new AuthResult
        {
            Success = true,
            Token = tokenHandler.WriteToken(token)
        };
    }
}