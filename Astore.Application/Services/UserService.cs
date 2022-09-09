using Astore.Domain;
using Astore.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Astore.Application.Services;

public class UserService : IUserService
{
    private readonly StoreDbContext _dbContext;

    public UserService(StoreDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    private async Task<User?> GetUserWithProfileAsync(Guid userId)
    {
        return await _dbContext.Users
            .Include(user => user.Profile)
            .SingleOrDefaultAsync(user => user.Id == userId);
    }
    
    public async Task<UserProfile?> GetProfileByIdAsync(Guid userId)
    {
        var user = await GetUserWithProfileAsync(userId);
        if (user == null)
            return null;

        return user.Profile;
    }

    public async Task<bool> UpdateProfileAsync(Guid userId, UserProfile profile)
    {
        var user = await GetUserWithProfileAsync(userId);
        if (user == null)
            return false;

        user.Profile = profile;
        return await _dbContext.SaveChangesAsync() > 0;
    }
}