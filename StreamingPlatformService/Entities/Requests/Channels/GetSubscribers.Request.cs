using System.Text.Json.Serialization;

namespace StreamingPlatformService.Entities.Requests.Channels;

public class GetSubscribersRequest
{
    [JsonPropertyName("channelId")]
    public Guid ChannelId { get; set; }
}