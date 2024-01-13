using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace StreamingPlatformService.Entities.Requests;

public class PatchPasswordRequest
{
    [Required]
    [JsonPropertyName("email")]
    public string Email { get; set; }
    
    [Required]
    [JsonPropertyName("old_password")]
    public string OldPassword { get; set; }
    
    [Required]
    [JsonPropertyName("new_password")]
    public string NewPassword { get; set; }
}