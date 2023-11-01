using Core.DTOs.Category;
using Core.Interfaces.Services;
using Core.Utilities.Constants;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers;

[ApiController]
[Route("categories")]
public class CategoriesController : Controller
{
    private readonly ICategoryService _categoryService;

    public CategoriesController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    [HttpGet("{categoryId}")]
    public async Task<IActionResult> GetCategory(Guid categoryId)
    {
        var categoryResult = await _categoryService.GetCategoryByIdAsync(categoryId, default);

        return HandleCategoryResult(categoryResult);
    }

    [HttpGet("with-products/{categoryId}")]
    public async Task<IActionResult> GetCategoryWithProducts(Guid categoryId)
    {
        var categoryResult = await _categoryService.GetCategoryWithProductsAsync(categoryId, default);

        return HandleCategoryResult(categoryResult);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllCategories()
    {
        var categoriesResult = await _categoryService.GetAllCategoriesAsync(p => true, default);

        return HandleCategoryResult(categoriesResult);
    }

    [HttpPost]
    public async Task<IActionResult> CreateCategory(CreateCategoryDto createCategoryDto)
    {
        var createCategoryResult = await _categoryService.CreateCategoryAsync(createCategoryDto, default);

        return HandleCategoryResult(createCategoryResult);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateCategory(UpdateCategoryDto updateCategoryDto)
    {
        var updateCategoryResult = await _categoryService.UpdateCategoryAsync(updateCategoryDto, default);

        return HandleCategoryResult(updateCategoryResult);
    }

    [HttpDelete("{categoryId}")]
    public async Task<IActionResult> DeleteCategory(Guid categoryId)
    {
        var deleteCategoryResult = await _categoryService.DeleteCategoryAsync(categoryId, default);

        return HandleCategoryResult(deleteCategoryResult);
    }

    #region Helper Methods
    private IActionResult HandleCategoryResult(Core.Utilities.Results.IResult result)
    {
        if (result.Success)
        {
            return Ok(result);
        }

        if (result.Message == Messages.CategoryNotFound
            || result.Message == Messages.EmptyCategoryList)
        {
            return NotFound(result);
        }

        return BadRequest(result);
    } 
    #endregion
}
