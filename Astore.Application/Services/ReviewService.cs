using Astore.Domain;
using Astore.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Astore.Application.Services;

public class ReviewService : IReviewService
{
    private readonly StoreDbContext _dbContext;

    public ReviewService(StoreDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    private async Task<Article?> GetArticleWithReviews(Guid articleId)
    {
        return await _dbContext.Articles
            .Include(article => article.Reviews)
            .SingleOrDefaultAsync(article => article.Id == articleId);
    }
    
    public async Task<bool> PostReviewAsync(Review review)
    {
        await _dbContext.Reviews.AddAsync(review);
        return await _dbContext.SaveChangesAsync() > 0;
    }

    public async Task<ICollection<Review>> GetAllReviewsAsync(Guid articleId)
    {
        var article = await GetArticleWithReviews(articleId);
        if (article == null)
            return (ICollection<Review>)Enumerable.Empty<Review>();

        return article.Reviews;
    }

    public async Task<Review?> GetReviewByIdAsync(Guid reviewId)
    {
        return await _dbContext.Reviews.FirstOrDefaultAsync(review => review.Id == reviewId);
    }

    public async Task<bool> UpdateReviewAsync(Review review)
    {
        _dbContext.Reviews.Update(review);
        return await _dbContext.SaveChangesAsync() > 0;
    }

    public async Task<bool> DeleteReviewAsync(Guid reviewId)
    {
        var review = await GetReviewByIdAsync(reviewId);
        if (review == null)
            return false;
        
        _dbContext.Reviews.Remove(review);
        return await _dbContext.SaveChangesAsync() > 0;
    }
}