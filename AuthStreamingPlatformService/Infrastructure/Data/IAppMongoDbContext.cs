using MongoDB.Driver;

namespace AuthStreamingPlatformService.Infrastructure.Data;

public interface IAppMongoDbContext
{
    IMongoDatabase GetDatabase();
}