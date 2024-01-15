using Microsoft.AspNetCore.Mvc;

namespace StreamingPlatformService.Entities.Requests.Channels;

public class GetChannelRequest
{
    public const string Route = "/channel/get-channel/{id:guid}";
    
    [FromRoute(Name = "id")]
    public Guid ChannelId { get; set; }
}