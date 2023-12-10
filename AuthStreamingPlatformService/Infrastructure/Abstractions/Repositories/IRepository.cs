using AuthStreamingPlatformService.Entities.Abstractions;
using TechDaily.Infrastructure.Abstractions.Repositories;

namespace AuthStreamingPlatformService.Infrastructure.Abstractions.Repositories;

public interface IRepository<T> : IRepositoryBase<T> where T : class, IEntityBase<Guid>
{
}