using MongoDB.Driver;
using StreamingPlatformService.Core.Abstractions.Errors;
using StreamingPlatformService.Core.Abstractions.Services.Categories;
using StreamingPlatformService.Core.Abstractions.Services.Channels;
using StreamingPlatformService.Core.Abstractions.Services.Users;
using StreamingPlatformService.Core.Errors;
using StreamingPlatformService.Entities;
using StreamingPlatformService.Entities.Requests.Channels;
using StreamingPlatformService.Infrastructure.Data;

namespace StreamingPlatformService.Core.Services.Channels;

public class ChannelsService : IChannelsService
{
    private readonly IAppMongoDbContext _mongoDbContext;
    private readonly IUsersService _usersService;
    private readonly ICategoriesService _categoriesService;
    private readonly IMongoCollection<Channel> _channels;
    
    public ChannelsService(IAppMongoDbContext mongoDbContext, IUsersService usersService, ICategoriesService categoriesService)
    {
        _mongoDbContext = mongoDbContext;
        _usersService = usersService;
        _categoriesService = categoriesService;
        _channels = _mongoDbContext.GetDatabase().GetCollection<Channel>("Channels");
    }
    
    public async Task<ErrorOr<IEnumerable<Channel>>> GetChannels()
    {
        var channels = await _channels.Find(_ => true).ToListAsync();
        return channels ?? new List<Channel>();
    }

    public async Task<ErrorOr<Channel>> GetChannel(Guid id)
    {
        throw new NotImplementedException();
    }

    public async Task<IErrorOr> DeleteChannel(Guid id)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Post Channel
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="request"></param>
    /// <returns></returns>
    public async Task<ErrorOr<Channel>> PostChannel(Guid userId, PostChannelRequest request)
    {
        var user = await _usersService.GetUser(userId);
        if (user.IsError)
            return user.FirstError;

        var category = await _categoriesService.GetCategory(request.CategoryId);
        if (category.IsError)
            return category.FirstError;
        
        var channels = await GetChannels();
        if (channels.IsError)
            return channels.FirstError;

        if (channels.Value.Any(x => x.User.Id == user.Value.Id))
            return Error.Conflict("Channels.Conflict", "Channel with this user already exist");
        
        var channel = new Channel
        {
            User = user.Value,
            Category = category.Value,
            SubscribersCount = 0
        };
        
        await _channels.InsertOneAsync(channel);

        return channel;
    }
}