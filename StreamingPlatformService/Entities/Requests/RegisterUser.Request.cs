using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace StreamingPlatformService.Entities.Requests;

public class RegisterUserRequest
{
    [Required]
    [JsonPropertyName("nick")]
    public string Nick { get; set; }
    
    [Required]
    [JsonPropertyName("first_name")]
    public string FirstName { get; set; }
    
    [Required]
    [JsonPropertyName("last_name")]
    public string LastName { get; set; }
    
    [Required] 
    [EmailAddress] 
    [JsonPropertyName("email")]
    public string Email { get; set; }
    
    [Required] 
    [JsonPropertyName("password")]
    public string Password { get; set; }
    
    [Required]
    [JsonPropertyName("logo")]
    public int Logo { get; set; }
}