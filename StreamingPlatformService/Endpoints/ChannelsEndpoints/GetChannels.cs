using Microsoft.AspNetCore.Mvc;
using StreamingPlatformService.Core.Abstractions.Services.Channels;
using StreamingPlatformService.Entities.Requests.Channels;
using StreamingPlatformService.Entities.Responses.Channels;
using StreamingPlatformService.Infrastructure.Endpoints;

namespace StreamingPlatformService.Endpoints.ChannelsEndpoints;

/// <summary>
/// Get channels endpoint
/// </summary>
public class GetChannels : EndpointBaseAsync.WithoutRequest.WithActionResult<GetChannelsResponse>
{
    private readonly IChannelsService _channelsService;

    /// <summary>
    /// Ctor for endpoint
    /// </summary>
    /// <param name="channelsService"></param>
    public GetChannels(IChannelsService channelsService)
    {
        _channelsService = channelsService;
    }
    
    [HttpGet("/channel/channels")]
    [ApiExplorerSettings(GroupName = "Channel")]
    public override async Task<ActionResult<GetChannelsResponse>> HandleAsync(CancellationToken cancellationToken = default)
    {
        var channels = await _channelsService.GetChannels();
        
        return channels.IsError ? 
            GetActionResult(channels) :
            Ok(new GetChannelsResponse
            {
                Channels = channels.Value
            });
    }
}