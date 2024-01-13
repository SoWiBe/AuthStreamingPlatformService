using MongoDB.Driver;

namespace StreamingPlatformService.Infrastructure.Data;

public interface IAppMongoDbContext
{
    IMongoDatabase GetDatabase();
}