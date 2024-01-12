using System.Text.Json.Serialization;

namespace AuthStreamingPlatformService.Entities.Requests;

public class PatchUserRequest
{
    [JsonPropertyName("nick")]
    public string? Nick { get; set; }
    
    [JsonPropertyName("first_name")]
    public string? FirstName { get; set; }
    
    [JsonPropertyName("last_name")]
    public string? LastName { get; set; }
}