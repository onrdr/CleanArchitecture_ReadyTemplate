using ApplicationCore.DTOs.Product;
using ApplicationCore.Entities;
using ApplicationCore.Utilities.Constants;
using ApplicationCore.Utilities.Results;
using FluentAssertions;
using Integration.Base;
using Integration.Fixtures;
using Integration.Harness;

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

        // Act
        var result = await ProductService.GetProductByIdAsync(product.Id, default); 

        // Assert
        result.Should().NotBeNull().And.BeOfType<SuccessDataResult<ViewProductDto>>();
        result.Data.Id.Should().Be(product.Id);
        result.Data.Name.Should().Be(product.Name);
        result.Data.Description.Should().Be(product.Description);
        result.Data.Price.Should().Be(product.Price);
        result.Data.CategoryId.Should().Be(product.CategoryId);
    } 
    #endregion
}
