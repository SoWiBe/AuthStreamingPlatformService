using Microsoft.AspNetCore.Mvc;
using StreamingPlatformService.Core.Abstractions.Services.Channels;
using StreamingPlatformService.Entities.Requests.Channels;
using StreamingPlatformService.Entities.Responses.Channels;
using StreamingPlatformService.Infrastructure.Endpoints;

namespace StreamingPlatformService.Endpoints.ChannelsEndpoints;

/// <summary>
/// Patch Channel Endpoint
/// </summary>
public class PatchChannel : EndpointBaseAsync.WithRequest<PatchChannelRequest>.WithActionResult<PatchChannelResponse>
{
    private readonly IChannelsService _channelsService;

    /// <summary>
    /// Ctor for endpoint
    /// </summary>
    public PatchChannel(IChannelsService channelsService)
    {
        _channelsService = channelsService;
    }
    
    [HttpPatch("/channel/patch-channel")]
    [ApiExplorerSettings(GroupName = "Channel")]
    public override async Task<ActionResult<PatchChannelResponse>> HandleAsync(PatchChannelRequest request,
        CancellationToken cancellationToken = default)
    {
        var channel = await _channelsService.PatchChannel(request);
        
        return channel.IsError ? GetActionResult(channel) : Ok(new PatchChannelResponse { Channel = channel.Value });
    }
}