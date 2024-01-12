﻿using System.Text.Json.Serialization;

namespace AuthStreamingPlatformService.Entities.Responses;

public class LoginUserResponse
{
    [JsonPropertyName("access")]
    public string Token { get; set; }
    
    [JsonPropertyName("expiration")]
    public DateTime Expiration { get; set; }
}