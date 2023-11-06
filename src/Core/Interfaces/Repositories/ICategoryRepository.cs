using ApplicationCore.Entities;
using ApplicationCore.Interfaces.Repositories.Base; 

namespace ApplicationCore.Interfaces.Repositories;

public interface ICategoryRepository : IBaseRepository<Category> 
{
    Task<Category?> GetCategoryWithProductsAsync(Guid categoryId);
}
