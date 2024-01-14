namespace StreamingPlatformService.Entities.Responses.Channels;

public class GetChannelsResponse
{
    public IEnumerable<Channel> Channels { get; set; }
}