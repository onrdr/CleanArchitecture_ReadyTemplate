using Core.Entities;
using Core.Interfaces.Repositories;
using FluentAssertions;
using Integration.Base;
using Microsoft.Extensions.DependencyInjection;

namespace Integration.Harness;

internal static class ProductHarness
{
    internal static async Task<Product> RegisterCategoryAndGetRandomProductAsync(this TestBase testBase, bool assertSuccess = true)
    {
        var productRepository = testBase.ApplicationFixture.Services.GetRequiredService<IProductRepository>();
        var productCounter = Guid.NewGuid().ToString()[..5];
        var category = await testBase.RegisterAndGetRandomCategoryAsync();

        var productToAdd = new Product()
        {
            Name = $"Category {productCounter}",
            Description = $"Category Description {productCounter}",
            Price = new Random().NextDouble() * 10,
            CategoryId = category.Id,
        };

        var registerResult = await productRepository.AddAsync(productToAdd);
        AssertRegisterResult(assertSuccess, registerResult);

        return productToAdd;
    }

    private static void AssertRegisterResult(bool assertSuccess, int registerResult)
    {
        if (assertSuccess)
            registerResult.Should().BeGreaterThan(0);
    }
}
