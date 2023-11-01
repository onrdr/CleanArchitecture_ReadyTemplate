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
        result.Data.Id.Should().Be(category.Id);
        result.Data.Name.Should().Be(category.Name);
        result.Data.Description.Should().Be(category.Description); 
    } 
}
