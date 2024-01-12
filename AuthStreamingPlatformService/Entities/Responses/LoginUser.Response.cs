using System.Text.Json.Serialization;

namespace AuthStreamingPlatformService.Entities.Responses;

public class LoginUserResponse
{
    public TokenResult Token { get; set; } 
}