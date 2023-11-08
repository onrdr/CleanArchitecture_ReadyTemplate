using AutoMapper; 
using ApplicationCore.DTOs.Category;
using ApplicationCore.Entities;
using ApplicationCore.Interfaces.Repositories;
using ApplicationCore.Interfaces.Services;
using ApplicationCore.Utilities.Constants;
using ApplicationCore.Utilities.Results;
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
    public async Task<IDataResult<ViewCategoryDto>> GetCategoryByIdAsync(Guid categoryId, CancellationToken cancellationToken)
    {
        var category = await _categoryRepository.GetByIdAsync(categoryId);
        return category is not null
            ? new SuccessDataResult<ViewCategoryDto>(_mapper.Map<ViewCategoryDto>(category))
            : new ErrorDataResult<ViewCategoryDto>(Messages.CategoryNotFound);
    }

    public async Task<IDataResult<ViewCategoryWithProductsDto>> GetCategoryWithProductsAsync(Guid categoryId, CancellationToken cancellationToken)
    {
        var category = await _categoryRepository.GetCategoryWithProductsAsync(categoryId);
        if (category is null)
        {
            return new ErrorDataResult<ViewCategoryWithProductsDto>(Messages.CategoryNotFound);
        }

        return !category.Products.Any() 
            ? new ErrorDataResult<ViewCategoryWithProductsDto>(Messages.EmptyProductListForCategoryError)
            : new SuccessDataResult<ViewCategoryWithProductsDto>(_mapper.Map<ViewCategoryWithProductsDto>(category)); 
    }

    public async Task<IDataResult<IEnumerable<ViewCategoryDto>>> GetAllCategoriesAsync(
        Expression<Func<Category, bool>> predicate, CancellationToken cancellationToken)
    {
        var categoryList = await _categoryRepository.GetAllAsync(predicate);
        return categoryList is not null && categoryList.Any()
            ? new SuccessDataResult<IEnumerable<ViewCategoryDto>>(_mapper.Map<IEnumerable<ViewCategoryDto>>(categoryList))
            : new ErrorDataResult<IEnumerable<ViewCategoryDto>>(Messages.EmptyCategoryList);
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
        var category= await _categoryRepository.GetByIdAsync(updateCategoryDto.Id);
        if (category is null)
        {
            return new ErrorResult(Messages.CategoryNotFound);
        } 

        var existResult = await CheckIfCategoryNameAlreadyExistsAsync(updateCategoryDto.Name, updateCategoryDto.Id);
        if (!existResult.Success)
        {
            return existResult;
        }

        CompleteUpdate(updateCategoryDto, category);

        var updateCategoryResult = await _categoryRepository.UpdateAsync(category);
        return updateCategoryResult > 0
            ? new SuccessResult(Messages.UpdateCategorySuccess)
            : new ErrorResult(Messages.UpdateCategoryError);
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
    private static void CompleteUpdate(UpdateCategoryDto updateCategoryDto, Category category)
    {
        category.Name = updateCategoryDto.Name;
        category.Description = updateCategoryDto.Description;
    }

    private async Task<IResult> CheckIfCategoryNameAlreadyExistsAsync(string name)
    { 
        var categoryList = await _categoryRepository.GetAllAsync(c => 
            c.Name.ToLower() == name.ToLower().Trim());

        return categoryList is not null && !categoryList.Any()
            ? new SuccessResult()
            : new ErrorResult(Messages.CategoryAlreadyExists);
    }

    private async Task<IResult> CheckIfCategoryNameAlreadyExistsAsync(string name, Guid categoryId)
    {
        var categoryList = await _categoryRepository.GetAllAsync(c =>
            c.Name.ToLower() == name.ToLower().Trim() && c.Id != categoryId);

        return categoryList is not null && !categoryList.Any()
            ? new SuccessResult()
            : new ErrorResult(Messages.CategoryAlreadyExists);
    }
    #endregion
}
