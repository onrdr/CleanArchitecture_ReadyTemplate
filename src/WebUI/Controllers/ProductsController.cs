using Core.DTOs.Product;
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
        var productResult = await _productService.GetProductByIdAsync(productId, default);

        return HandleProductResult(productResult);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllProducts()
    {
        var productsResult = await _productService.GetAllProductsAsync(p => true, default);

        return HandleProductResult(productsResult);
    }

    [HttpPost]
    public async Task<IActionResult> CreateProduct(CreateProductDto createProductDto)
    {
        var createProductResult = await _productService.CreateProductAsync(createProductDto, default);

        return HandleProductResult(createProductResult);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateProduct(UpdateProductDto updateProductDto)
    {
        var updateProductResult = await _productService.UpdateProductAsync(updateProductDto, default);

        return HandleProductResult(updateProductResult);
    }

    [HttpDelete("{productId}")]
    public async Task<IActionResult> DeleteProduct(Guid productId)
    {
        var deleteProductResult = await _productService.DeleteProductAsync(productId, default);

        return HandleProductResult(deleteProductResult);
    }

    #region Helper Methods
    private IActionResult HandleProductResult(Core.Utilities.Results.IResult result)
    {
        if (result.Success)
        {
            return Ok(result);
        }

        if (result.Message == Messages.ProductNotFound
            || result.Message == Messages.EmptyProductList)
        {
            return NotFound(result);
        }

        return BadRequest(result);
    }
    #endregion
}
