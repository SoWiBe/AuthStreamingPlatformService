using System.Linq.Expressions;

namespace StreamingPlatformService.Infrastructure.Abstractions.Repositories;

public interface IReadRepositoryBase<T>
{
    Task<T?> GetByIdAsync<TId>(TId id, CancellationToken cancellationToken = default) where TId : notnull;
    Task<List<T>> ListAsync(CancellationToken cancellationToken = default);
    Task<List<T>> GetByIdsAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default);
}