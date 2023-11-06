namespace Integration.ServiceTests.ProductServiceTests;

public class ProductServiceTests : TestBase
{
    public ProductServiceTests(ApplicationFixture applicationFixture) : base(applicationFixture) { }

    #region GetProduct
    [Fact]
    public async Task GetProductByIdAsync_WhenProductDoesNotExist_ReturnsErrorDataResult()
    {
        // Arrange
        var productId = Guid.NewGuid();

        // Act
        var result = await ProductService.GetProductByIdAsync(productId, default);

        // Assert
        result.Should().NotBeNull().And.BeOfType<ErrorDataResult<ViewProductDto>>();
        result.Message.Should().Be(Messages.ProductNotFound);
    }

    [Fact]
    public async Task GetProductByIdAsync_WhenProductExist_ReturnsSuccessDataResult()
    {
        // Arrange
        var product = await this.RegisterCategoryAndGetRandomProductAsync();
        var viewProductDto = this.ConvertProductToViewProductDto(product);
        // Act
        var result = await ProductService.GetProductByIdAsync(product.Id, default); 

        // Assert
        result.Should().NotBeNull().And.BeOfType<SuccessDataResult<ViewProductDto>>();
        result.Data.Should().BeEquivalentTo(viewProductDto); 
    } 
    #endregion
}
