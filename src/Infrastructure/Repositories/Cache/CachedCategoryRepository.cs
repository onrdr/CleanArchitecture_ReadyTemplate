using ApplicationCore.Entities;
using ApplicationCore.Interfaces.Repositories; 
using Infrastructure.Repositories;
using Microsoft.Extensions.Caching.Memory;
using System.Linq.Expressions;

namespace DataAccess.Repositories.Concrete.Cache;

public class CachedCategoryRepository(
    CategoryRepository _decorated, 
    IMemoryCache _cache) : ICategoryRepository
{
    private static readonly List<string> CachedKeys = [];

    public async Task<Category?> GetByIdAsync(Guid id)
    {
        string key = $"category-{id}";
        return await _cache.GetOrCreateAsync(key, async entry =>
        {
            CachedKeys.Add(key);
            entry.SetAbsoluteExpiration(TimeSpan.FromMinutes(3));
            return await _decorated.GetByIdAsync(id);
        });
    }

    public async Task<IEnumerable<Category>?> GetAllAsync(Expression<Func<Category, bool>> predicate)
    {
        string key = $"all-categories-{predicate}";
        return await _cache.GetOrCreateAsync(key, async entry =>
        {
            CachedKeys.Add(key);
            entry.SetAbsoluteExpiration(TimeSpan.FromMinutes(3));
            return await _decorated.GetAllAsync(predicate);
        });
    }

    public async Task<Category?> GetCategoryWithProductsAsync(Guid productId)
    {
        string key = $"with-product-{productId}";
        return await _cache.GetOrCreateAsync(key, async entry =>
        {
            CachedKeys.Add(key);
            entry.SetAbsoluteExpiration(TimeSpan.FromMinutes(3));
            return await _decorated.GetCategoryWithProductsAsync(productId);
        });
    }

    public async Task<int> AddAsync(Category entity)
    {
        int result = await _decorated.AddAsync(entity);
        RemoveAllCachedItems(result);
        return result;
    } 

    public async Task<int> UpdateAsync(Category entity)
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
