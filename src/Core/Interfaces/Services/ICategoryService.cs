using ApplicationCore.Entities;
using ApplicationCore.Utilities.Results;
using System.Linq.Expressions;
using ApplicationCore.DTOs.Category; 

namespace ApplicationCore.Interfaces.Services;

public interface ICategoryService
{ 
    Task<IDataResult<ViewCategoryDto>> GetCategoryByIdAsync(Guid categoryId, CancellationToken cancellationToken);
    Task<IDataResult<ViewCategoryWithProductsDto>> GetCategoryWithProductsAsync(Guid categoryId, CancellationToken cancellationToken);
    Task<IDataResult<IEnumerable<ViewCategoryDto>>> GetAllCategoriesAsync(Expression<Func<Category, bool>> predicate, CancellationToken cancellationToken);
    Task<IResult> CreateCategoryAsync(CreateCategoryDto createCategoryDto, CancellationToken cancellationToken);
    Task<IResult> UpdateCategoryAsync(UpdateCategoryDto updateCategoryDto, CancellationToken cancellationToken);
    Task<IResult> DeleteCategoryAsync(Guid categoryId, CancellationToken cancellationToken);
}
