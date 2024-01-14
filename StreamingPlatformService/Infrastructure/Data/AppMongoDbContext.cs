using MongoDB.Bson;
using MongoDB.Driver;

namespace StreamingPlatformService.Infrastructure.Data;

public class AppMongoDbContext : IAppMongoDbContext
{
    private readonly MongoClient _client;

    public AppMongoDbContext(string connection)
    {
        BsonDefaults.GuidRepresentation = GuidRepresentation.Standard;
        _client = new MongoClient(connection);
    }
    
    public IMongoDatabase GetDatabase() => _client.GetDatabase("StreamingPlatform");
}