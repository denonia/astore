﻿using Astore.Domain;

namespace Astore.Application;

public interface ICategoryService
{
    Task<Category?> GetCategoryAsync(Guid categoryId);
}