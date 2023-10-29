using Core.Entities.DTOs.Category;
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
        var result = await _categoryService.GetCategoryByIdAsync(categoryId, default);

        return HandleCategoryResult(result);
    }

    [HttpGet("with-products/{categoryId}")]
    public async Task<IActionResult> GetCategoryWithProducts(Guid categoryId)
    {
        var result = await _categoryService.GetCategoryWithProductsAsync(categoryId, default);

        return HandleCategoryResult(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllCategories()
    {
        var result = await _categoryService.GetAllCategoriesAsync(p => true, default);

        return HandleCategoryResult(result);
    }

    [HttpPost]
    public async Task<IActionResult> CreateCategory(CreateCategoryDto createCategoryDto)
    {
        var result = await _categoryService.CreateCategoryAsync(createCategoryDto, default);

        return HandleCategoryResult(result);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateCategory(UpdateCategoryDto updateCategoryDto)
    {
        var result = await _categoryService.UpdateCategoryAsync(updateCategoryDto, default);

        return HandleCategoryResult(result);
    }

    [HttpDelete("{categoryId}")]
    public async Task<IActionResult> DeleteCategory(Guid categoryId)
    {
        var result = await _categoryService.DeleteCategoryAsync(categoryId, default);

        return HandleCategoryResult(result);
    }

    #region Helper Methods
    private IActionResult HandleCategoryResult(Core.Utilities.Results.IResult categoryResult)
    {
        if (categoryResult.Success)
            return Ok(categoryResult);

        if (categoryResult.Message == Messages.CategoryNotFound
            || categoryResult.Message == Messages.EmptyCategoryList)
        {
            return NotFound(categoryResult);
        }

        return BadRequest(categoryResult);
    } 
    #endregion
}
