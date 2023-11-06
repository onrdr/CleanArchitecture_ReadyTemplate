namespace Integration.Harness;

internal static class CategoryHarness
{ 
    internal static async Task<Category> RegisterAndGetRandomCategoryAsync(this TestBase testBase, bool assertSuccess = true)
    {
        var categoryRepository = testBase.ApplicationFixture.Services.GetRequiredService<ICategoryRepository>(); 
        var categoryCounter = Guid.NewGuid().ToString()[..5];

        var categoryToAdd = new Category()
        {
            Name = $"Category {categoryCounter}", 
            Description = $"Category Description {categoryCounter}"
        };

        var registerResult = await categoryRepository.AddAsync(categoryToAdd);
        AssertRegisterResult(assertSuccess, registerResult);

        return categoryToAdd;
    }

    private static void AssertRegisterResult(bool assertSuccess, int registerResult)
    {
        if (assertSuccess)
            registerResult.Should().BeGreaterThan(0);
    }

    public static ViewCategoryDto ConvertCategoryToViewCategoryDto(this TestBase testBase, Category category)
    {
        return new ViewCategoryDto()
        {
            Id = category.Id,
            Name = category.Name,
            Description = category.Description,
        };
    }

    internal static async Task<Category> RegisterCategoryWithProductsAndGetCategoryAsync(this TestBase testBase, bool assertSuccess = true)
    {
        var categoryRepository = testBase.ApplicationFixture.Services.GetRequiredService<ICategoryRepository>();
        var product = await testBase.RegisterCategoryAndGetRandomProductAsync();
        var category = await categoryRepository.GetCategoryWithProductsAsync(product.CategoryId);

        AssertGetCategoryByIdResult(true, category);

        return category!;
    }

    public static ViewCategoryWithProductsDto ConvertCategoryToViewCategoryWithProductsDto(this TestBase testBase, Category category)
    {
        return new ViewCategoryWithProductsDto
        {
            Id = category.Id,
            Name = category.Name,
            Description = category.Description,
            Products = category.Products,
        };
    }

    private static void AssertGetCategoryByIdResult(bool assertSuccess, Category? category)
    {
        if (assertSuccess)
        {
            category.Should().NotBeNull();
            category?.Products.Should().NotBeNull().And.NotBeEmpty();
        }
    }
}
