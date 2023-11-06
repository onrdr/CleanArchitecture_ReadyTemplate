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
        var viewCategoryDto = this.ConvertCategoryToViewCategoryDto(category);

        // Act
        var result = await CategoryService.GetCategoryByIdAsync(category.Id, default);

        // Assert
        result.Should().NotBeNull().And.BeOfType<SuccessDataResult<ViewCategoryDto>>();
        result.Data.Should().BeEquivalentTo(viewCategoryDto);
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
        var viewCategoryList = this.ConvertCategoryToViewCategoryWithProductsDto(category);
        // Act
        var result = await CategoryService.GetCategoryWithProductsAsync(category.Id, default);

        // Assert
        result.Should().NotBeNull().And.BeOfType<SuccessDataResult<ViewCategoryWithProductsDto>>();
        result.Data.Should().BeEquivalentTo(viewCategoryList);
    }
    #endregion
}
