using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace StreamingPlatformService.Entities.Requests.Channels;

public class PatchChannelRequest
{
    [Required]
    [JsonPropertyName("id")]
    public Guid Id { get; set; }
    
    [JsonPropertyName("category")]
    public Guid CategoryId { get; set; }
}