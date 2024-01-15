using Microsoft.AspNetCore.Mvc;
using StreamingPlatformService.Core.Abstractions.Services.Channels;
using StreamingPlatformService.Entities.Requests.Channels;
using StreamingPlatformService.Entities.Responses.Channels;
using StreamingPlatformService.Infrastructure.Endpoints;

namespace StreamingPlatformService.Endpoints.ChannelsEndpoints;

/// <summary>
/// Get Channel Endpoint
/// </summary>
public class GetChannel : EndpointBaseAsync.WithRequest<GetChannelRequest>.WithActionResult<GetChannelResponse>
{
    private readonly IChannelsService _channelsService;

    /// <summary>
    /// Ctor for get channel
    /// </summary>
    /// <param name="channelsService"></param>
    public GetChannel(IChannelsService channelsService)
    {
        _channelsService = channelsService;
    }
    
    [HttpGet(GetChannelRequest.Route)]
    [ApiExplorerSettings(GroupName = "Channel")]
    public override async Task<ActionResult<GetChannelResponse>> HandleAsync([FromRoute]GetChannelRequest request, 
        CancellationToken cancellationToken = default)
    {
        var channel = await _channelsService.GetChannel(request.ChannelId);

        return channel.IsError ? 
            GetActionResult(channel) : 
            Ok(new GetChannelResponse {Channel = channel.Value});
    }
}