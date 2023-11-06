using ApplicationCore.DTOs.Category;
using ApplicationCore.Utilities.Constants;
using ApplicationCore.Utilities.Results;
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
        result.Should().NotBeNull().And.BeOfType<ErrorDataResult<ViewCategoryDto>>();
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
        result.Should().NotBeNull().And.BeOfType<SuccessDataResult<ViewCategoryDto>>();
        result.Data.Id.Should().Be(category.Id);
        result.Data.Name.Should().Be(category.Name);
        result.Data.Description.Should().Be(category.Description); 
    }

    [Fact]
    public async Task GetCategoryWithProductsAsync_WhenCategoryDoesNotExist_ReturnsErrorDataResult()
    {
        // Arrange
        var categoryId = Guid.NewGuid();

        // Act
        var result = await CategoryService.GetCategoryWithProductsAsync(categoryId, default);

        // Assert
        result.Should().NotBeNull().And.BeOfType<ErrorDataResult<ViewCategoryWithProductsDto>>();
        result.Message.Should().Be(Messages.CategoryNotFound);
    }

    [Fact]
    public async Task GetCategoryWithProductsAsync_WhenCategoryExistButDoesNotHaveAnyProducts_ReturnsErrorDataResult()
    {
        // Arrange
        var category = await this.RegisterAndGetRandomCategoryAsync();

        // Act
        var result = await CategoryService.GetCategoryWithProductsAsync(category.Id, default);

        // Assert
        result.Should().NotBeNull().And.BeOfType<ErrorDataResult<ViewCategoryWithProductsDto>>();
        result.Data.Should().BeNull();
        result.Message.Should().Be(Messages.EmptyProductListForCategoryError);
    }

    [Fact]
    public async Task GetCategoryWithProductsAsync_WhenCategoryExistAndProductsIncluded_ReturnsSuccessDataResult()
    {
        // Arrange
        var category = await this.RegisterCategoryWithProductsAndGetCategoryAsync();

        // Act
        var result = await CategoryService.GetCategoryWithProductsAsync(category.Id, default);

        // Assert
        result.Should().NotBeNull().And.BeOfType<SuccessDataResult<ViewCategoryWithProductsDto>>();
        result.Data.Id.Should().Be(category.Id);
        result.Data.Name.Should().Be(category.Name);
        result.Data.Description.Should().Be(category.Description);
        result.Data.Products.Should().BeEquivalentTo(category.Products);
    }
    #endregion
}
