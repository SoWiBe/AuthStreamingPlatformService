using Microsoft.AspNetCore.Mvc;
using StreamingPlatformService.Core.Abstractions.Services.Channels;
using StreamingPlatformService.Entities.Requests.Channels;
using StreamingPlatformService.Entities.Responses.Channels;
using StreamingPlatformService.Infrastructure.Endpoints;

namespace StreamingPlatformService.Endpoints.ChannelsEndpoints;

/// <summary>
/// Get subscribers endpoint
/// </summary>
public class GetSubscribers : EndpointBaseAsync.WithRequest<GetSubscribersRequest>.WithActionResult<GetSubscribersResponse>
{
    private readonly IChannelsService _channelsService;

    /// <summary>
    /// Ctor for endpoint
    /// </summary>
    /// <param name="channelsService"></param>
    public GetSubscribers(IChannelsService channelsService)
    {
        _channelsService = channelsService;
    }
    
    [HttpGet("/channel/get-subscribers")] 
    [ApiExplorerSettings(GroupName = "Channel")]
    public override async Task<ActionResult<GetSubscribersResponse>> HandleAsync(GetSubscribersRequest request, 
        CancellationToken cancellationToken = default)
    {
        var subscribers = await _channelsService.GetSubscribers(request.ChannelId);
        return subscribers.IsError ? 
            GetActionResult(subscribers) : 
            Ok(new GetSubscribersResponse { Users = subscribers.Value});
    }
}