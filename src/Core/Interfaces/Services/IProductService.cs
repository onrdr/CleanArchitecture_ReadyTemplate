using ApplicationCore.Entities;
using ApplicationCore.Utilities.Results;
using System.Linq.Expressions;
using ApplicationCore.DTOs.Product; 

namespace ApplicationCore.Interfaces.Services;

public interface IProductService
{
    Task<IDataResult<ViewProductDto>> GetProductByIdAsync(Guid productId, CancellationToken cancellationToken);
    Task<IDataResult<IEnumerable<ViewProductDto>>> GetAllProductsAsync(Expression<Func<Product, bool>> predicate, CancellationToken cancellationToken);
    Task<IResult> CreateProductAsync(CreateProductDto createProductDto, CancellationToken cancellationToken);
    Task<IResult> UpdateProductAsync(UpdateProductDto updateProductDto, CancellationToken cancellationToken);
    Task<IResult> DeleteProductAsync(Guid productId, CancellationToken cancellationToken);
}
