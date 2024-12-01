using ApplicationCore.Entities;
using ApplicationCore.Interfaces.Repositories;
using Infrastructure.Data;
using Infrastructure.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class CategoryRepository(ApplicationDbContext context) : BaseRepository<Category>(context), ICategoryRepository
{
    public async Task<Category?> GetCategoryWithProductsAsync(Guid categoryId)
    {
        var category = await _dataContext.Categories
            .Where(c => c.Id == categoryId)
            .Include(c => c.Products)
            .FirstOrDefaultAsync();

        return category;
    }
}
