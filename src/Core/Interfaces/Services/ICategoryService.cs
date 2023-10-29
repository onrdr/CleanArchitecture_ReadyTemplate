using Core.Entities.DTOs.Category;
using Core.Entities; 
using Core.Utilities.Results;
using System.Linq.Expressions;
namespace Core.Interfaces.Services;

public interface ICategoryService
{ 
    Task<IDataResult<Category>> GetCategoryByIdAsync(Guid categoryId, CancellationToken cancellationToken);
    Task<IDataResult<Category>> GetCategoryWithProductsAsync(Guid categoryId, CancellationToken cancellationToken);
    Task<IDataResult<IEnumerable<Category>>> GetAllCategoriesAsync(Expression<Func<Category, bool>> predicate, CancellationToken cancellationToken);
    Task<IResult> CreateCategoryAsync(CreateCategoryDto createCategoryDto, CancellationToken cancellationToken);
    Task<IResult> UpdateCategoryAsync(UpdateCategoryDto updateCategoryDto, CancellationToken cancellationToken);
    Task<IResult> DeleteCategoryAsync(Guid categoryId, CancellationToken cancellationToken);
}
