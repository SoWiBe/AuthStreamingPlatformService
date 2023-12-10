using System.Linq.Expressions;
using AuthStreamingPlatformService.Entities.Abstractions;
using AuthStreamingPlatformService.Infrastructure.Abstractions.Repositories;
using Microsoft.EntityFrameworkCore;

namespace AuthStreamingPlatformService.Infrastructure.Repositories;

public class RepositoryBase<T> : IReadRepository<T>, IRepository<T> where T : class, IEntityBase<Guid>
{
    private readonly DbContext _dbContext;

    public RepositoryBase(DbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public virtual async Task<T?> GetByIdAsync<TId>(TId id, CancellationToken cancellationToken = default)
        where TId : notnull
    {
        return await _dbContext.Set<T>().FindAsync(new object[] { id }, cancellationToken);
    }
    
    public virtual async Task<T?> GetByIdsAsync<TId>(TId[] id, CancellationToken cancellationToken = default)
        where TId : notnull
    {
        return await _dbContext.Set<T>().FindAsync(id, cancellationToken);
    }
    
    
    public virtual async Task<List<T>> ListAsync(CancellationToken cancellationToken = default)
    {
        return await _dbContext.Set<T>().ToListAsync(cancellationToken);
    }

    public virtual async Task<List<T>> GetByIdsAsync(Expression<Func<T, bool>> predicate,
        CancellationToken cancellationToken = default)
    {
        //
        return await _dbContext.Set<T>().Where(predicate).ToListAsync(cancellationToken);
    }   

    public virtual async Task<T> AddAsync(T entity, CancellationToken cancellationToken = default)
    {
        _dbContext.Set<T>().Add(entity);

        await SaveChangesAsync(cancellationToken);

        return entity;
    }

    public virtual async Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entities,
        CancellationToken cancellationToken = default)
    {
        _dbContext.Set<T>().AddRange(entities);

        await SaveChangesAsync(cancellationToken);

        return entities;
    }

    public virtual async Task UpdateAsync(T entity, CancellationToken cancellationToken = default)
    {
        _dbContext.Set<T>().Update(entity);

        await SaveChangesAsync(cancellationToken);
    }

    public virtual async Task UpdateRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default)
    {
        _dbContext.Set<T>().UpdateRange(entities);

        await SaveChangesAsync(cancellationToken);
    }

    public virtual async Task DeleteAsync(T entity, CancellationToken cancellationToken = default)
    {
        _dbContext.Set<T>().Remove(entity);

        await SaveChangesAsync(cancellationToken);
    }

    public virtual async Task DeleteAsync(Guid entityId, CancellationToken cancellationToken = default)
    {
        var entity = Expression.Lambda<Func<T>>(Expression.New(typeof(T))).Compile().Invoke();
        entity.Id = entityId;

        _dbContext.Set<T>().Remove(entity);

        await SaveChangesAsync(cancellationToken);
    }

    public virtual async Task DeleteRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default)
    {
        _dbContext.Set<T>().RemoveRange(entities);

        await SaveChangesAsync(cancellationToken);
    }

    public virtual async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await _dbContext.SaveChangesAsync(cancellationToken);
    }
}