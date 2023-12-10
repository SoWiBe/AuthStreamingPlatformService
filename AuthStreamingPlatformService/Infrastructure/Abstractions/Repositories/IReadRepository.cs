using TechDaily.Entities;
using TechDaily.Entities.Abstractions;

namespace TechDaily.Infrastructure.Abstractions.Repositories;

public interface IReadRepository<T> : IReadRepositoryBase<T> where T : class, IEntityBase<Guid>
{
}