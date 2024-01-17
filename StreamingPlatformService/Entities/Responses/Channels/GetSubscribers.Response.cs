using System.Text.Json.Serialization;

namespace StreamingPlatformService.Entities.Responses.Channels;

public class GetSubscribersResponse
{
    [JsonPropertyName("subscribers")]
    public IEnumerable<User> Users { get; set; }
}