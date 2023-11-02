using AutoMapper;
using Core.DTOs.Product;
using Core.Entities;
using Core.Interfaces.Repositories;
using Core.Interfaces.Services;
using Core.Utilities.Constants;
using Core.Utilities.Results;
using System.Linq.Expressions;

namespace Infrastructure.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;
    private readonly ICategoryService _categoryService;
    private readonly IMapper _mapper;

    public ProductService(IProductRepository productRepository, IMapper mapper, ICategoryService categoryService)
    {
        _productRepository = productRepository;
        _mapper = mapper;
        _categoryService = categoryService;
    }

    #region Read
    public async Task<IDataResult<Product>> GetProductByIdAsync(Guid productId, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetByIdAsync(productId);
        return product is not null
            ? new SuccessDataResult<Product>(product)
            : new ErrorDataResult<Product>(Messages.ProductNotFound);
    }

    public async Task<IDataResult<IEnumerable<Product>>> GetAllProductsAsync(
        Expression<Func<Product, bool>> predicate, CancellationToken cancellationToken)
    {
        var productList = await _productRepository.GetAllAsync(predicate);
        return productList is not null && productList.Any()
            ? new SuccessDataResult<IEnumerable<Product>>(productList)
            : new ErrorDataResult<IEnumerable<Product>>(Messages.EmptyProductList);
    }
    #endregion

    #region Create
    public async Task<IResult> CreateProductAsync(CreateProductDto createProductDto, CancellationToken cancellationToken)
    {
        var categoryResult = await _categoryService.GetCategoryByIdAsync(createProductDto.CategoryId, cancellationToken);
        if (!categoryResult.Success)
        {
            return categoryResult;
        }

        var createCategoryResult = await _productRepository.AddAsync(_mapper.Map<Product>(createProductDto));
        return createCategoryResult > 0
            ? new SuccessResult(Messages.CreateProductSuccess)
            : new ErrorResult(Messages.CreateProductError);
    }
    #endregion

    #region Update
    public async Task<IResult> UpdateProductAsync(UpdateProductDto updateProductDto, CancellationToken cancellationToken)
    {
        var productResult = await GetProductByIdAsync(updateProductDto.Id, cancellationToken);
        if (!productResult.Success)
        {
            return productResult;
        }

        var categoryResult = await _categoryService.GetCategoryByIdAsync(updateProductDto.CategoryId, cancellationToken);
        if (!categoryResult.Success)
        {
            return categoryResult;
        }

        CompleteUpdate(productResult.Data, updateProductDto);

        var updateProductResult = await _productRepository.UpdateAsync(productResult.Data);
        return updateProductResult > 0
            ? new SuccessResult(Messages.UpdateProductSuccess)
            : new ErrorResult(Messages.UpdateProductError);
    }

    private static void CompleteUpdate(Product product, UpdateProductDto updateProductDto)
    {
        product.Name = updateProductDto.Name;
        product.Description = updateProductDto.Description;
        product.Price = updateProductDto.Price;
        product.CategoryId = updateProductDto.CategoryId;
    }

    #endregion

    #region Delete
    public async Task<IResult> DeleteProductAsync(Guid productId, CancellationToken cancellationToken)
    {
        var productResult = await GetProductByIdAsync(productId, cancellationToken);
        if (!productResult.Success)
        {
            return productResult;
        }

        var deleteProductResult = await _productRepository.DeleteAsync(productId);
        return deleteProductResult > 0
            ? new SuccessResult(Messages.DeleteProductSuccess)
            : new ErrorResult(Messages.DeleteProductError);
    }
    #endregion
}
