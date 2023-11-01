using Core.Entities;
using Core.Interfaces.Repositories;
using FluentAssertions;
using Integration.Base;
using Microsoft.Extensions.DependencyInjection;

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
}
