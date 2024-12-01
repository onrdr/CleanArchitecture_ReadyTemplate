using ApplicationCore.DTOs.Product;
using ApplicationCore.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers;


[ApiController]
[Route("products")]
public class ProductsController(IProductService _productService) : BaseController
{
    [HttpGet("{productId:guid}")]
    public async Task<IActionResult> GetProduct(Guid productId)
    {
        var productResult = await _productService.GetProductByIdAsync(productId, default);

        return HandleResponse(productResult);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllProducts()
    {
        var productsResult = await _productService.GetAllProductsAsync(p => true, default);

        return HandleResponse(productsResult);
    }

    [HttpPost]
    public async Task<IActionResult> CreateProduct(CreateProductDto createProductDto)
    {
        var createProductResult = await _productService.CreateProductAsync(createProductDto, default);

        return HandleResponse(createProductResult);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateProduct(UpdateProductDto updateProductDto)
    {
        var updateProductResult = await _productService.UpdateProductAsync(updateProductDto, default);

        return HandleResponse(updateProductResult);
    }

    [HttpDelete("{productId}")]
    public async Task<IActionResult> DeleteProduct(Guid productId)
    {
        var deleteProductResult = await _productService.DeleteProductAsync(productId, default);

        return HandleResponse(deleteProductResult);
    }
}
