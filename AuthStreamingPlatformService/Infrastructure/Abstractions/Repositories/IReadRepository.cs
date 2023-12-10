using AuthStreamingPlatformService.Entities.Abstractions;
using TechDaily.Infrastructure.Abstractions.Repositories;

namespace AuthStreamingPlatformService.Infrastructure.Abstractions.Repositories;

public interface IReadRepository<T> : IReadRepositoryBase<T> where T : class, IEntityBase<Guid>
{
}