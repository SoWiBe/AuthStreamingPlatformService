using Microsoft.AspNetCore.Mvc;
using StreamingPlatformService.Core.Abstractions.Errors;
using StreamingPlatformService.Core.Abstractions.Services.Channels;
using StreamingPlatformService.Entities.Responses.Channels;
using StreamingPlatformService.Infrastructure.Endpoints;

namespace StreamingPlatformService.Endpoints.ChannelsEndpoints;

/// <summary>
/// Get User Channel endpoint
/// </summary>
public class GetUserChannel : EndpointBaseAsync.WithoutRequest.WithActionResult<GetChannelResponse>
{
    private readonly IChannelsService _channelsService;

    /// <summary>
    /// Ctor for user channel
    /// </summary>
    /// <param name="channelsService"></param>
    public GetUserChannel(IChannelsService channelsService)
    {
        _channelsService = channelsService;
    }
    
    [HttpGet("/channel/get-channel")]
    [ApiExplorerSettings(GroupName = "Channel")]
    public override async Task<ActionResult<GetChannelResponse>> HandleAsync(CancellationToken cancellationToken = default)
    {
        var rawId = User.Claims.FirstOrDefault(c => c.Type == "sub")?.Value;
        if (string.IsNullOrEmpty(rawId))
            return BadRequest(Error.Custom(5, "User.BadRequest", "User not found"));

        var channel = await _channelsService.GetChannelByUser(new Guid(rawId));
        return channel.IsError ? 
            GetActionResult(channel) : 
            Ok(new GetChannelResponse { Channel = channel.Value });
    }
}