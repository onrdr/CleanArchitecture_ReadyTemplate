using ApplicationCore.DTOs.Category;
using ApplicationCore.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers;

[ApiController]
[Route("categories")]
public class CategoriesController(ICategoryService _categoryService) : BaseController
{
    [HttpGet("{categoryId}")]
    public async Task<IActionResult> GetCategory(Guid categoryId)
    {
        var categoryResult = await _categoryService.GetCategoryByIdAsync(categoryId, default);

        return HandleResponse(categoryResult);
    }

    [HttpGet("with-products/{categoryId}")]
    public async Task<IActionResult> GetCategoryWithProducts(Guid categoryId)
    {
        var categoryResult = await _categoryService.GetCategoryWithProductsAsync(categoryId, default);

        return HandleResponse(categoryResult);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllCategories()
    {
        var categoriesResult = await _categoryService.GetAllCategoriesAsync(p => true, default);

        return HandleResponse(categoriesResult);
    }

    [HttpPost]
    public async Task<IActionResult> CreateCategory(CreateCategoryDto createCategoryDto)
    {
        var createCategoryResult = await _categoryService.CreateCategoryAsync(createCategoryDto, default);

        return HandleResponse(createCategoryResult);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateCategory(UpdateCategoryDto updateCategoryDto)
    {
        var updateCategoryResult = await _categoryService.UpdateCategoryAsync(updateCategoryDto, default);

        return HandleResponse(updateCategoryResult);
    }

    [HttpDelete("{categoryId}")]
    public async Task<IActionResult> DeleteCategory(Guid categoryId)
    {
        var deleteCategoryResult = await _categoryService.DeleteCategoryAsync(categoryId, default);

        return HandleResponse(deleteCategoryResult);
    }
}
