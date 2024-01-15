using Microsoft.AspNetCore.Mvc;
using StreamingPlatformService.Core.Abstractions.Services.Channels;
using StreamingPlatformService.Entities.Requests.Channels;
using StreamingPlatformService.Entities.Responses.Channels;
using StreamingPlatformService.Infrastructure.Endpoints;

namespace StreamingPlatformService.Endpoints.ChannelsEndpoints;

/// <summary>
/// Delete Channel endpoint
/// </summary>
public class DeleteChannel : EndpointBaseAsync.WithRequest<DeleteChannelRequest>.WithActionResult<DeleteChannelResponse>
{
    private readonly IChannelsService _channelsService;

    /// <summary>
    /// Ctor for endpoint
    /// </summary>
    /// <param name="channelsService"></param>
    public DeleteChannel(IChannelsService channelsService)
    {
        _channelsService = channelsService;
    }
    
    [HttpDelete(DeleteChannelRequest.Route)]
    [ApiExplorerSettings(GroupName = "Channel")]
    public override async Task<ActionResult<DeleteChannelResponse>> HandleAsync([FromRoute] DeleteChannelRequest request, 
        CancellationToken cancellationToken = default)
    {
        var result = await _channelsService.DeleteChannel(request.Id);
        
        return result.IsError ? GetActionResult(result) : Ok(new DeleteChannelResponse { Detail = "Success!" });
    }
}