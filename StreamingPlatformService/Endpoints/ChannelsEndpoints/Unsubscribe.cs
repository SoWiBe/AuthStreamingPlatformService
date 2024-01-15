using Microsoft.AspNetCore.Mvc;
using StreamingPlatformService.Core.Abstractions.Errors;
using StreamingPlatformService.Core.Abstractions.Services.Channels;
using StreamingPlatformService.Entities.Requests.Channels;
using StreamingPlatformService.Entities.Responses.Channels;
using StreamingPlatformService.Infrastructure.Endpoints;

namespace StreamingPlatformService.Endpoints.ChannelsEndpoints;

/// <summary>
/// Unsubscribe endpoint
/// </summary>
public class Unsubscribe : EndpointBaseAsync.WithRequest<UnsubscribeRequest>.WithActionResult<UnsubscribeResponse>
{
    private readonly IChannelsService _channelsService;

    /// <summary>
    /// Ctor for endpoint
    /// </summary>
    /// <param name="channelsService"></param>
    public Unsubscribe(IChannelsService channelsService)
    {
        _channelsService = channelsService;
    }

    [HttpPost("channel/unsubscribe")]
    [ApiExplorerSettings(GroupName = "Channel")]
    public override async Task<ActionResult<UnsubscribeResponse>> HandleAsync(UnsubscribeRequest request,
        CancellationToken cancellationToken = default)
    {
        var rawId = User.Claims.FirstOrDefault(c => c.Type == "sub")?.Value;
        if (string.IsNullOrEmpty(rawId))
            return BadRequest(Error.Custom(5, "User.BadRequest", "User not found"));

        var result = await _channelsService.UnSubscribe(new Guid(rawId), request.ChannelId);

        return result.IsError ? GetActionResult(result) : Ok(new UnsubscribeResponse { Detail = "Success!" });
        
        
    }
}