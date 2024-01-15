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
    private readonly IMongoCollection<Channel> _channels;
    
    private readonly IUsersService _usersService;
    private readonly ICategoriesService _categoriesService;
    
    
    public ChannelsService(IAppMongoDbContext mongoDbContext, IUsersService usersService, ICategoriesService categoriesService)
    {
        _mongoDbContext = mongoDbContext;
        _usersService = usersService;
        _categoriesService = categoriesService;
        _channels = _mongoDbContext.GetDatabase().GetCollection<Channel>("Channels");
    }
    
    /// <summary>
    /// Get All Channels
    /// </summary>
    /// <returns></returns>
    public async Task<ErrorOr<IEnumerable<Channel>>> GetChannels(Guid? categoryId = null)
    {
        if (categoryId is null)
            return await _channels.Find(_ => true).ToListAsync();

        var category = await _categoriesService.GetCategory((Guid)categoryId);
        if (category.IsError)
            return category.FirstError;
        
        var filter = Builders<Channel>.Filter.Eq("category", category.Value);
        var channels = await _channels.Find(filter).ToListAsync();

        return channels ?? new List<Channel>();
    }

    /// <summary>
    /// Get Channel by id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<ErrorOr<Channel>> GetChannel(Guid id)
    {
        var filter = Builders<Channel>.Filter.Eq("_id", id);

        var channel = await _channels.Find(filter).FirstOrDefaultAsync();
        if (channel is null)
            return Error.NotFound("Channel.Error", "Channel not found");

        return channel;
    }

    
    /// <summary>
    /// Get Channel by category
    /// </summary>
    /// <param name="categoryId"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public async Task<ErrorOr<Channel>> GetChannelByCategory(Guid categoryId)
    {
        var category = await _categoriesService.GetCategory(categoryId);
        if (category.IsError)
            return category.FirstError;
        
        var filter = Builders<Channel>.Filter.Eq("category", category);
        
        var channel = await _channels.Find(filter).FirstOrDefaultAsync();
        if (channel is null)
            return Error.NotFound("Channel.Error", "Channel not found");

        return channel;
    }

    /// <summary>
    /// Get Channel by user
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public async Task<ErrorOr<Channel>> GetChannelByUser(Guid userId)
    {
        var user = await _usersService.GetUser(userId);
        if (user.IsError)
            return user.FirstError;
        
        var filter = Builders<Channel>.Filter.Eq("user", user.Value);
        
        var channel = await _channels.Find(filter).FirstOrDefaultAsync();
        if (channel is null)
            return Error.NotFound("Channel.Error", "Channel not found");

        return channel;
    }

    /// <summary>
    /// Delete channel
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public async Task<IErrorOr> DeleteChannel(Guid id)
    {
        var filter = Builders<Channel>.Filter.Eq("_id", id);
        
        await _channels.DeleteOneAsync(filter);

        return ErrorOr.NoError();
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

    /// <summary>
    /// Patch Channel
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    public async Task<ErrorOr<Channel>> PatchChannel(PatchChannelRequest request)
    {
        var filter = Builders<Channel>.Filter.Eq("_id", request.Id);

        var category = await _categoriesService.GetCategory(request.CategoryId);
        if (category.IsError)
            return category.FirstError;

        var update = Builders<Channel>.Update.Set("category", category.Value);
        await UpdateField(filter, update);

        var channel = await GetChannel(request.Id);
        if (channel.IsError)
            return channel.FirstError;

        return channel.Value;
    }
    
    private async Task UpdateField(FilterDefinition<Channel> filter, UpdateDefinition<Channel> update)
    {
        await _channels.UpdateOneAsync(filter, update);
    }
}