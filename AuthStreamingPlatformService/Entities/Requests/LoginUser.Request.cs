using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace AuthStreamingPlatformService.Entities.Requests;

public class LoginUserRequest
{
    [Required]
    [JsonPropertyName("email")]
    public string Email { get; set; }
    
    [Required]
    [JsonPropertyName("password")]
    public string Password { get; set; }
}