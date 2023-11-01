using Core.Entities;
using Core.Utilities.Results;
using System.Linq.Expressions;
using Core.DTOs.Product;

namespace Core.Interfaces.Services;

public interface IProductService
{
    Task<IDataResult<Product>> GetProductByIdAsync(Guid productId, CancellationToken cancellationToken);
    Task<IDataResult<IEnumerable<Product>>> GetAllProductsAsync(Expression<Func<Product, bool>> predicate, CancellationToken cancellationToken);
    Task<IResult> CreateProductAsync(CreateProductDto createProductDto, CancellationToken cancellationToken);
    Task<IResult> UpdateProductAsync(UpdateProductDto updateProductDto, CancellationToken cancellationToken);
    Task<IResult> DeleteProductAsync(Guid productId, CancellationToken cancellationToken);
}
