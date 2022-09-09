using Astore.Domain;

namespace Astore.Application;

public interface IUserService
{
    Task<UserProfile?> GetProfileByIdAsync(Guid userId);
    Task<bool> UpdateProfileAsync(Guid userId, UserProfile profile);
}