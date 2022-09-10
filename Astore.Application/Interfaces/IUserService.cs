using Astore.Domain;

namespace Astore.Application;

public interface IUserService
{
    Task<UserProfile?> GetUserProfileAsync(Guid userId);
    Task<bool> UpdateUserProfileAsync(UserProfile userProfile);
}