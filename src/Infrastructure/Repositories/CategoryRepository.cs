using Core.Entities;
using Core.Interfaces.Repositories;
using Infrastructure.Data;
using Infrastructure.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
{
    public CategoryRepository(ApplicationDbContext context) : base(context)
    {

    }

    public async Task<Category?> GetCategoryWithProductsAsync(Guid categoryId)
    {
        var category = await _dataContext.Categories
            .Where(c => c.Id == categoryId)
            .Include(c => c.Products)
            .FirstOrDefaultAsync();

        return category;
    }
}
