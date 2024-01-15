using Microsoft.AspNetCore.Mvc;

namespace StreamingPlatformService.Entities.Requests.Channels;

public class DeleteChannelRequest
{
    public const string Route = "/channel/delete-channel/{id:guid}";
    
    [FromRoute(Name = "id")]
    public Guid Id { get; set; }
}