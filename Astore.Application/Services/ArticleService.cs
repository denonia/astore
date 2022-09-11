using Astore.Domain;
using Astore.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Astore.Application.Services;

public class ArticleService : IArticleService
{
    private readonly StoreDbContext _dbContext;

    public ArticleService(StoreDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<bool> CreateArticleAsync(Article article)
    {
        await _dbContext.Articles.AddAsync(article);
        return await _dbContext.SaveChangesAsync() > 0;
    }

    public async Task<ICollection<Article>> GetArticlesAsync()
    {
        return await _dbContext.Articles
            .Include(article => article.Category)
            .ToListAsync();
    }

    public async Task<Article?> GetArticleByIdAsync(Guid id)
    {
        return await _dbContext.Articles
            .Include(article => article.Reviews)
            .ThenInclude(review => review.Author)
            .Include(article => article.Category)
            .SingleOrDefaultAsync(article => article.Id == id);
    }

    public async Task<bool> UpdateArticleAsync(Article article)
    {
        _dbContext.Articles.Update(article);
        return await _dbContext.SaveChangesAsync() > 0;
    }

    public async Task<bool> DeleteArticleAsync(Guid id)
    {
        var article = await GetArticleByIdAsync(id);
        if (article == null)
            return false;

        _dbContext.Articles.Remove(article);
        return await _dbContext.SaveChangesAsync() > 0;
    }
}