using System.Text.Json.Serialization;

namespace StreamingPlatformService.Entities.Responses;

public class RegisterUserResponse
{
    [JsonPropertyName("access")]
    public string Token { get; set; }
    
    [JsonPropertyName("expiration")]
    public DateTime Expiration { get; set; }
}