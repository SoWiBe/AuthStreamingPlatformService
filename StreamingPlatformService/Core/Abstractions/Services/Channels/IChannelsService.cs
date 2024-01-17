using StreamingPlatformService.Core.Abstractions.Errors;
using StreamingPlatformService.Core.Errors;
using StreamingPlatformService.Entities;
using StreamingPlatformService.Entities.Requests.Categories;
using StreamingPlatformService.Entities.Requests.Channels;

namespace StreamingPlatformService.Core.Abstractions.Services.Channels;

public interface IChannelsService
{
    Task<ErrorOr<IEnumerable<Channel>>> GetChannels(Guid? categoryId = null);
    Task<ErrorOr<Channel>> GetChannel(Guid id);
    Task<ErrorOr<Channel>> GetChannelByCategory(Guid categoryId);
    Task<ErrorOr<Channel>> GetChannelByUser(Guid userId);
    Task<IErrorOr> DeleteChannel(Guid id);
    Task<ErrorOr<Channel>> PostChannel(Guid userId, PostChannelRequest request);
    Task<ErrorOr<Channel>> PatchChannel(PatchChannelRequest request);
    Task<IErrorOr> Subscribe(Guid userId, Guid channelId);
    Task<ErrorOr<IEnumerable<User>>> GetSubscribers(Guid channelId);
}