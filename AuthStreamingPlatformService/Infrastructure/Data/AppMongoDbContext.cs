using AuthStreamingPlatformService.Core.Abstractions.Errors;
using AuthStreamingPlatformService.Core.Errors;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;

namespace AuthStreamingPlatformService.Infrastructure.Data;

public class AppMongoDbContext : IAppMongoDbContext
{
    private readonly MongoClient _client;

    public AppMongoDbContext(string connection)
    {
        _client = new MongoClient(connection);
    }

    public IMongoDatabase GetDatabase() => _client.GetDatabase("StreamingPlatform");
}