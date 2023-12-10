using TechDaily.Entities;
using TechDaily.Entities.Abstractions;

namespace TechDaily.Infrastructure.Abstractions.Repositories;

public interface IRepository<T> : IRepositoryBase<T> where T : class, IEntityBase<Guid>
{
}