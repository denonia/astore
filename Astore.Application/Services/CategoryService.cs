using Astore.Domain;
using Astore.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Astore.Application.Services;

public class CategoryService : ICategoryService
{
    private readonly StoreDbContext _dbContext;

    public CategoryService(StoreDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<Category?> GetCategoryAsync(Guid categoryId)
    {
        return await _dbContext.Categories.SingleOrDefaultAsync(category => category.Id == categoryId);
    }
}