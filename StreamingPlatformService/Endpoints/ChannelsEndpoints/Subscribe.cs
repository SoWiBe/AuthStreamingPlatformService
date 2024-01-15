using Microsoft.AspNetCore.Mvc;
using StreamingPlatformService.Core.Abstractions.Errors;
using StreamingPlatformService.Core.Abstractions.Services.Channels;
using StreamingPlatformService.Entities.Requests.Channels;
using StreamingPlatformService.Entities.Responses.Channels;
using StreamingPlatformService.Infrastructure.Endpoints;

namespace StreamingPlatformService.Endpoints.ChannelsEndpoints;

/// <summary>
/// Subscribe endpoint
/// </summary>
public class Subscribe : EndpointBaseAsync.WithRequest<SubscribeRequest>.WithActionResult<SubscribeResponse>
{
    private readonly IChannelsService _channelsService;

    /// <summary>
    /// Ctor for endpoint
    /// </summary>
    /// <param name="channelsService"></param>
    public Subscribe(IChannelsService channelsService)
    {
        _channelsService = channelsService;
    }
    
    [HttpPost(SubscribeRequest.Route)]
    [ApiExplorerSettings(GroupName = "Channel")]
    public override async Task<ActionResult<SubscribeResponse>> HandleAsync(SubscribeRequest request, 
        CancellationToken cancellationToken = default)
    {
        var rawId = User.Claims.FirstOrDefault(c => c.Type == "sub")?.Value;
        if (string.IsNullOrEmpty(rawId))
            return BadRequest(Error.Custom(5, "User.BadRequest", "User not found"));

        var result = await _channelsService.Subscribe(new Guid(rawId), request.ChannelId);

        return result.IsError ? GetActionResult(result) : Ok(new SubscribeResponse { Detail = "Success!" });
    }
}