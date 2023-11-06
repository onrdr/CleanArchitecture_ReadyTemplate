using ApplicationCore.Entities;
using ApplicationCore.Interfaces.Repositories;
using Infrastructure.Repositories;
using Microsoft.Extensions.Caching.Memory; 
using System.Linq.Expressions;

namespace DataAccess.Repositories.Concrete.Cache;

public class CachedProductRepository : IProductRepository
{
    private readonly IMemoryCache _cache;
    private readonly ProductRepository _decorated;
    private static readonly List<string> CachedKeys = new();

    public CachedProductRepository(ProductRepository decorated, IMemoryCache cache)
    {
        _decorated = decorated;
        _cache = cache;
    }

    public async Task<Product?> GetByIdAsync(Guid id)
    {
        string key = $"product-{id}";
        return await _cache.GetOrCreateAsync(key, async entry =>
        {
            CachedKeys.Add(key);
            entry.SetAbsoluteExpiration(TimeSpan.FromMinutes(3));
            return await _decorated.GetByIdAsync(id);
        });
    }

    public async Task<IEnumerable<Product>?> GetAllAsync(Expression<Func<Product, bool>> predicate)
    {
        string key = $"all-products-{predicate}";
        return await _cache.GetOrCreateAsync(key, async entry =>
        {
            CachedKeys.Add(key);
            entry.SetAbsoluteExpiration(TimeSpan.FromMinutes(3));
            return await _decorated.GetAllAsync(predicate);
        });
    } 

    public async Task<int> AddAsync(Product entity)
    {
        int result = await _decorated.AddAsync(entity);
        RemoveAllCachedItems(result);
        return result;
    } 

    public async Task<int> UpdateAsync(Product entity)
    {
        var result = await _decorated.UpdateAsync(entity);
        RemoveAllCachedItems(result);
        return result;
    }

    public async Task<int> DeleteAsync(Guid id)
    {
        var result = await _decorated.DeleteAsync(id);
        RemoveAllCachedItems(result);
        return result;
    } 

    #region Helper Methods
    private void RemoveAllCachedItems(int result)
    {
        if (result > 0)
        {
            foreach (var key in CachedKeys)
            {
                _cache.Remove(key);
            }
        }

        CachedKeys.Clear();
    } 
    #endregion
}
