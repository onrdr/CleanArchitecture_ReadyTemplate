using Core.Entities;
using Core.Interfaces.Repositories.Base; 

namespace Core.Interfaces.Repositories;

public interface ICategoryRepository : IBaseRepository<Category> 
{
    Task<Category?> GetCategoryWithProductsAsync(Guid categoryId);
}
