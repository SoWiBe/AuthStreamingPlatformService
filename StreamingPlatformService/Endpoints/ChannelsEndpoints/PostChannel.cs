using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using StreamingPlatformService.Core.Abstractions.Errors;
using StreamingPlatformService.Core.Abstractions.Services.Channels;
using StreamingPlatformService.Core.Abstractions.Services.Users;
using StreamingPlatformService.Entities;
using StreamingPlatformService.Entities.Requests.Channels;
using StreamingPlatformService.Entities.Responses.Channels;
using StreamingPlatformService.Infrastructure.Endpoints;

namespace StreamingPlatformService.Endpoints.ChannelsEndpoints;

/// <summary>
/// Post Channel endpoint
/// </summary>
public class PostChannel : EndpointBaseAsync.WithRequest<PostChannelRequest>.WithActionResult<PostChannelResponse>
{
    private readonly IChannelsService _channelsService;

    /// <summary>
    /// Ctor for endpoint 
    /// </summary>
    /// <param name="channelsService"></param>
    public PostChannel(IChannelsService channelsService)
    {
        _channelsService = channelsService;
    }
    
    [HttpPost("/channel/post-channel")]
    [ApiExplorerSettings(GroupName = "Channel")]
    public override async Task<ActionResult<PostChannelResponse>> HandleAsync(PostChannelRequest request, 
        CancellationToken cancellationToken = default)
    {
        var rawId = User.Claims.FirstOrDefault(c => c.Type == "sub")?.Value;

        if (string.IsNullOrEmpty(rawId))
            return BadRequest(Error.Custom(5, "User.BadRequest", "User not found"));
        
        var result = await _channelsService.PostChannel(new Guid(rawId), request);
        
        return result.IsError ? GetActionResult(result) : Ok(new PostChannelResponse { Channel = result.Value });
    }
}