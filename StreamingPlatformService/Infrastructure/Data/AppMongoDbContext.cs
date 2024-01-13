using MongoDB.Driver;

namespace StreamingPlatformService.Infrastructure.Data;

public class AppMongoDbContext : IAppMongoDbContext
{
    private readonly MongoClient _client;

    public AppMongoDbContext(string connection)
    {
        _client = new MongoClient(connection);
    }

    public IMongoDatabase GetDatabase() => _client.GetDatabase("StreamingPlatform");
}