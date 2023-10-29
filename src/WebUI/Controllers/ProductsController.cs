using Core.Entities.DTOs.Product;
using Core.Interfaces.Services;
using Core.Utilities.Constants;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers;


[ApiController]
[Route("products")]
public class ProductsController : Controller
{
    private readonly IProductService _productService;

    public ProductsController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpGet("{productId}")]
    public async Task<IActionResult> GetProduct(Guid productId)
    {
        var result = await _productService.GetProductByIdAsync(productId, default);

        return HandleProductResult(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllProducts()
    {
        var result = await _productService.GetAllProductsAsync(p => true, default);

        return HandleProductResult(result);
    }

    [HttpPost]
    public async Task<IActionResult> CreateProduct(CreateProductDto createProductDto)
    {
        var result = await _productService.CreateProductAsync(createProductDto, default);

        return HandleProductResult(result);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateProduct(UpdateProductDto updateProductDto)
    {
        var result = await _productService.UpdateProductAsync(updateProductDto, default);

        return HandleProductResult(result);
    }

    [HttpDelete("{productId}")]
    public async Task<IActionResult> DeleteProduct(Guid productId)
    {
        var result = await _productService.DeleteProductAsync(productId, default);

        return HandleProductResult(result);
    }

    #region Helper Methods
    private IActionResult HandleProductResult(Core.Utilities.Results.IResult productResult)
    {
        if (productResult.Success)
            return Ok(productResult);

        if (productResult.Message == Messages.ProductNotFound
            || productResult.Message == Messages.EmptyProductList)
        {
            return NotFound(productResult);
        }

        return BadRequest(productResult);
    }
    #endregion
}
