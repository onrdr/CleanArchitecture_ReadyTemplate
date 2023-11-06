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
            Name = $"Product {productCounter}",
            Description = $"Product Description {productCounter}",
            Price = new Random().NextDouble() * 10,
            CategoryId = category.Id,
            Category = category,
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

    public static ViewProductDto ConvertProductToViewProductDto(this TestBase testBase, Product product)
    {
        return new ViewProductDto
        {
            Id = product.Id,
            Name = product.Name,
            Description = product.Description,
            Price = product.Price,
            CategoryId = product.CategoryId,
        };
    }
}
