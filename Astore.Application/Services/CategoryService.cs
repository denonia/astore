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
    
    public async Task<Category> GetCategoryByNameAsync(string categoryName)
    {
        var category = await _dbContext.Categories.SingleOrDefaultAsync(category => category.Name == categoryName);
        if (category == null)
        {
            category = new Category { Name = categoryName };
            _dbContext.Categories.Add(category);
            await _dbContext.SaveChangesAsync();
        }
        return category;
    }
}