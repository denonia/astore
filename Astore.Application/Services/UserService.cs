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

    public async Task<UserProfile?> GetUserProfileAsync(Guid userId)
    {
        return await _dbContext.UserProfiles
            .Include(user => user.Favorites)
            .Include(user => user.Reviews)
            .Include(user => user.CartItems)
            .SingleOrDefaultAsync(user => user.UserId == userId);
    }

    public async Task<bool> UpdateUserProfileAsync(UserProfile userProfile)
    {
        _dbContext.UserProfiles.Update(userProfile);
        return await _dbContext.SaveChangesAsync() > 0;
    }
}