using Core.Entities;
using Core.Utilities.Constants;
using Core.Utilities.Results;
using FluentAssertions;
using Integration.Base;
using Integration.Fixtures;
using Integration.Harness;

namespace Integration.ServiceTests.ProductServiceTests;

public class ProductServiceTests : TestBase
{
    public ProductServiceTests(ApplicationFixture applicationFixture) : base(applicationFixture) { }

    [Fact]
    public async Task GetProductByIdAsync_WhenProductDoesNotExist_ReturnsErrorDataResult()
    {
        // Arrange
        var productId = Guid.NewGuid();

        // Act
        var result = await ProductService.GetProductByIdAsync(productId, default);

        // Assert
        result.Should().NotBeNull().And.BeOfType<ErrorDataResult<Product>>();
        result.Message.Should().Be(Messages.ProductNotFound);
    }

    [Fact]
    public async Task GetProductByIdAsync_WhenProductExist_ReturnsSuccessDataResult()
    {
        // Arrange
        var productToAdd = await this.RegisterCategoryAndGetRandomProductAsync();

        // Act
        var result = await ProductService.GetProductByIdAsync(productToAdd.Id, default); 

        // Assert
        result.Should().NotBeNull().And.BeOfType<SuccessDataResult<Product>>();
        result.Data.Id.Should().Be(productToAdd.Id);
        result.Data.Name.Should().Be(productToAdd.Name);
        result.Data.Description.Should().Be(productToAdd.Description);
        result.Data.Price.Should().Be(productToAdd.Price);
        result.Data.CategoryId.Should().Be(productToAdd.CategoryId);
    } 
}
