using Astore.Domain;

namespace Astore.Application;

public interface IReviewService
{
    Task<bool> PostReviewAsync(Review review);
    Task<ICollection<Review>> GetAllReviewsAsync(Guid articleId);
    Task<Review?> GetReviewByIdAsync(Guid reviewId);
    Task<bool> UpdateReviewAsync(Review review);
    Task<bool> DeleteReviewAsync(Guid reviewId);
}