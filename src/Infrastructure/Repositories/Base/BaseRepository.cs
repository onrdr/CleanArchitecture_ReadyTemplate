using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using ApplicationCore.Interfaces.Repositories.Base;
using ApplicationCore.Interfaces.Entities.Base;
using Infrastructure.Data;

namespace Infrastructure.Repositories.Base;

public class BaseRepository<T> : IBaseRepository<T>
    where T : class, IBaseEntity, new()
{
    protected readonly ApplicationDbContext _dataContext;
    private readonly DbSet<T> dbSet;

    public BaseRepository(ApplicationDbContext context)
    {
        _dataContext = context;
        dbSet = _dataContext.Set<T>();
    }

    public async Task<T?> GetByIdAsync(Guid id) 
        => await dbSet.AsNoTracking().FirstOrDefaultAsync(entity => entity.Id == id);

    public async Task<IEnumerable<T>?> GetAllAsync(Expression<Func<T, bool>> predicate) 
        => await dbSet.AsNoTracking().Where(predicate).ToListAsync();  
    
    public async Task<int> AddAsync(T entity)
    {
        await dbSet.AddAsync(entity);
        return await _dataContext.SaveChangesAsync();
    }

    public async Task<int> UpdateAsync(T entity)
    {
        _dataContext.Update(entity);
        return await _dataContext.SaveChangesAsync();
    }

    public async Task<int> DeleteAsync(Guid id)
    {
        var entity = await dbSet.FindAsync(id);

        if (entity != null)
        {
            _dataContext.Remove(entity);
            return await _dataContext.SaveChangesAsync();
        }
        return -1;
    } 
}
