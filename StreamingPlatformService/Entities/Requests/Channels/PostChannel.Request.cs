using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace StreamingPlatformService.Entities.Requests.Channels;

public class PostChannelRequest
{
    [Required] [JsonPropertyName("category")] public Guid CategoryId { get; set; }
}