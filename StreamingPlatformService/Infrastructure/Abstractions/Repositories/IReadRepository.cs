using StreamingPlatformService.Entities.Abstractions;

namespace StreamingPlatformService.Infrastructure.Abstractions.Repositories;

public interface IReadRepository<T> : IReadRepositoryBase<T> where T : class, IEntityBase<Guid>
{
}