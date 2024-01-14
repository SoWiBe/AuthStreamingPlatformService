using StreamingPlatformService.Core.Abstractions.Errors;
using StreamingPlatformService.Core.Errors;
using StreamingPlatformService.Entities;
using StreamingPlatformService.Entities.Requests.Categories;
using StreamingPlatformService.Entities.Requests.Channels;

namespace StreamingPlatformService.Core.Abstractions.Services.Channels;

public interface IChannelsService
{
    Task<ErrorOr<IEnumerable<Channel>>> GetChannels();
    Task<ErrorOr<Channel>> GetChannel(Guid id);
    Task<IErrorOr> DeleteChannel(Guid id);
    Task<ErrorOr<Channel>> PostChannel(Guid userId, PostChannelRequest request);
    // Task<ErrorOr<Category>> PatchCategory(PatchCategoryRequest request);
}