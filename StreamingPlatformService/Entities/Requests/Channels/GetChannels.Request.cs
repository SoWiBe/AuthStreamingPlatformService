using Microsoft.AspNetCore.Mvc;

namespace StreamingPlatformService.Entities.Requests.Channels;

public class GetChannelsRequest
{
    [FromQuery(Name = "category")]
    public Guid? CategoryId { get; set; }
}