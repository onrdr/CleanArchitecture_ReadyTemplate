using System.Linq.Expressions;
using ApplicationCore.Entities;
using ApplicationCore.DTOs.Product;
using ApplicationCore.Interfaces.Repositories;
using ApplicationCore.Interfaces.Services;
using ApplicationCore.Utilities.Constants;
using ApplicationCore.Utilities.Results;
using AutoMapper; 

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
    public async Task<IDataResult<ViewProductDto>> GetProductByIdAsync(Guid productId, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetByIdAsync(productId);
        return product is not null
            ? new SuccessDataResult<ViewProductDto>(_mapper.Map<ViewProductDto>(product))
            : new ErrorDataResult<ViewProductDto>(Messages.ProductNotFound);
    }

    public async Task<IDataResult<IEnumerable<ViewProductDto>>> GetAllProductsAsync(
        Expression<Func<Product, bool>> predicate, CancellationToken cancellationToken)
    {
        var productList = await _productRepository.GetAllAsync(predicate);
        return productList is not null && productList.Any()
            ? new SuccessDataResult<IEnumerable<ViewProductDto>>(_mapper.Map<IEnumerable<ViewProductDto>>(productList))
            : new ErrorDataResult<IEnumerable<ViewProductDto>>(Messages.EmptyProductList);
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
        var product = await _productRepository.GetByIdAsync(updateProductDto.Id);
        if (product is null)
        {
            return new ErrorResult(Messages.ProductNotFound);
        }

        var categoryResult = await _categoryService.GetCategoryByIdAsync(updateProductDto.CategoryId, cancellationToken);
        if (!categoryResult.Success)
        {
            return categoryResult;
        }

        CompleteUpdate(product, updateProductDto);

        var updateProductResult = await _productRepository.UpdateAsync(product);
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
