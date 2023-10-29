using AutoMapper;
using Core.Entities; 
using Core.Entities.DTOs.Category;
using Core.Interfaces.Repositories;
using Core.Interfaces.Services;
using Core.Utilities.Constants;
using Core.Utilities.Results;
using System.Linq.Expressions;

namespace Infrastructure.Services;

public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IMapper _mapper;

    public CategoryService(ICategoryRepository categoryRepository, IMapper mapper)
    {
        _categoryRepository = categoryRepository;
        _mapper = mapper;
    }

    #region Read
    public async Task<IDataResult<Category>> GetCategoryByIdAsync(Guid categoryId, CancellationToken cancellationToken)
    {
        var category = await _categoryRepository.GetByIdAsync(categoryId);
        return category is not null
            ? new SuccessDataResult<Category>(category)
            : new ErrorDataResult<Category>(Messages.CategoryNotFound);
    }

    public async Task<IDataResult<Category>> GetCategoryWithProductsAsync(Guid categoryId, CancellationToken cancellationToken)
    {
        var category = await _categoryRepository.GetCategoryWithProductsAsync(categoryId);
        return category is not null
            ? new SuccessDataResult<Category>(category)
            : new ErrorDataResult<Category>(Messages.CategoryNotFound);
    }

    public async Task<IDataResult<IEnumerable<Category>>> GetAllCategoriesAsync(
        Expression<Func<Category, bool>> predicate, CancellationToken cancellationToken)
    {
        var categoryList = await _categoryRepository.GetAllAsync(predicate);
        return categoryList is not null && categoryList.Any()
            ? new SuccessDataResult<IEnumerable<Category>>(categoryList)
            : new ErrorDataResult<IEnumerable<Category>>(Messages.EmptyCategoryList);
    }
    #endregion

    #region Create
    public async Task<IResult> CreateCategoryAsync(CreateCategoryDto createCategoryDto, CancellationToken cancellationToken)
    {
        var existResult = await CheckIfCategoryNameAlreadyExistsAsync(createCategoryDto.Name);
        if (!existResult.Success)
        {
            return existResult;
        }

        var createCategoryResult = await _categoryRepository.AddAsync(_mapper.Map<Category>(createCategoryDto));
        return createCategoryResult > 0
            ? new SuccessResult(Messages.CreateCategorySuccess)
            : new ErrorResult(Messages.CreateCategoryError);
    }
    #endregion

    #region Update
    public async Task<IResult> UpdateCategoryAsync(UpdateCategoryDto updateCategoryDto, CancellationToken cancellationToken)
    {
        var categoryResult = await GetCategoryByIdAsync(updateCategoryDto.Id, cancellationToken);
        if (!categoryResult.Success)
        {
            return categoryResult;
        }

        var existResult = await CheckIfCategoryNameAlreadyExistsAsync(updateCategoryDto.Name);
        if (!existResult.Success)
        {
            return existResult;
        }

        CompleteUpdate(updateCategoryDto, categoryResult.Data);

        var updateCategoryResult = await _categoryRepository.UpdateAsync(categoryResult.Data);
        return updateCategoryResult > 0
            ? new SuccessResult(Messages.UpdateCategorySuccess)
            : new ErrorResult(Messages.UpdateCategoryError);
    } 

    private static void CompleteUpdate(UpdateCategoryDto updateCategoryDto, Category category)
    {
        category.Name = updateCategoryDto.Name; 
        category.Description = updateCategoryDto.Description; 
    } 
    #endregion

    #region Delete
    public async Task<IResult> DeleteCategoryAsync(Guid categoryId, CancellationToken cancellationToken)
    {
        var categoryResult = await GetCategoryByIdAsync(categoryId, cancellationToken);
        if (!categoryResult.Success)
        {
            return categoryResult;
        }

        var deleteCategoryResult = await _categoryRepository.DeleteAsync(categoryId);
        return deleteCategoryResult > 0
            ? new SuccessResult(Messages.DeleteCategorySuccess)
            : new ErrorResult(Messages.DeleteCategoryError);
    }
    #endregion

    #region Helper Methods
    private async Task<IResult> CheckIfCategoryNameAlreadyExistsAsync(string name)
    {
        var categoryList = await _categoryRepository.GetAllAsync(c => c.Name.ToLower() == name.ToLower().Trim());
        return !categoryList.Any()
            ? new SuccessResult()
            : new ErrorResult(Messages.CategoryAlreadyExists);
    }
    #endregion
}
