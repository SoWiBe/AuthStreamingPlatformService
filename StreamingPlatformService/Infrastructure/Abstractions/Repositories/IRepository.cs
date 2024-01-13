using StreamingPlatformService.Entities.Abstractions;

namespace StreamingPlatformService.Infrastructure.Abstractions.Repositories;

public interface IRepository<T> : IRepositoryBase<T> where T : class, IEntityBase<Guid>
{
}