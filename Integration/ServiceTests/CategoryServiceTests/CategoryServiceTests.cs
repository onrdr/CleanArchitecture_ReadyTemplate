using Core.Entities;
using Core.Utilities.Constants;
using Core.Utilities.Results;
using FluentAssertions;
using Integration.Base;
using Integration.Fixtures;
using Integration.Harness;

namespace Integration.ServiceTests.CategoryServiceTests;

public class CategoryServiceTests : TestBase
{
    public CategoryServiceTests(ApplicationFixture applicationFixture) : base(applicationFixture) { }

    #region GetCategory
    [Fact]
    public async Task GetCategoryByIdAsync_WhenCategoryDoesNotExist_ReturnsErrorDataResult()
    {
        // Arrange
        var categoryId = Guid.NewGuid();

        // Act
        var result = await CategoryService.GetCategoryByIdAsync(categoryId, default);

        // Assert
        result.Should().NotBeNull().And.BeOfType<ErrorDataResult<Category>>();
        result.Message.Should().Be(Messages.CategoryNotFound);
    }

    [Fact]
    public async Task GetCategoryByIdAsync_WhenCategoryExist_ReturnsSuccessDataResult()
    {
        // Arrange
        var category = await this.RegisterAndGetRandomCategoryAsync();

        // Act
        var result = await CategoryService.GetCategoryByIdAsync(category.Id, default);

        // Assert
        result.Should().NotBeNull().And.BeOfType<SuccessDataResult<Category>>();
        result.Data.Should().BeEquivalentTo(category);
    }

    [Fact]
    public async Task GetCategoryWithProductsAsync_WhenCategoryDoesNotExist_ReturnsErrorDataResult()
    {
        // Arrange
        var categoryId = Guid.NewGuid();

        // Act
        var result = await CategoryService.GetCategoryWithProductsAsync(categoryId, default);

        // Assert
        result.Should().NotBeNull().And.BeOfType<ErrorDataResult<Category>>();
        result.Message.Should().Be(Messages.CategoryNotFound);
    }

    [Fact]
    public async Task GetCategoryWithProductsAsync_WhenCategoryExistAndProductsIncluded_ReturnsSuccessDataResult()
    {
        // Arrange
        var category = await this.RegisterCategoryWithProductsAndGetCategoryAsync();

        // Act
        var result = await CategoryService.GetCategoryByIdAsync(category.Id, default);

        // Assert
        result.Should().NotBeNull().And.BeOfType<SuccessDataResult<Category>>();
        result.Data.Should().BeEquivalentTo(category);
        result.Data.Products.Should().BeEquivalentTo(category.Products);
    }
    #endregion
}
