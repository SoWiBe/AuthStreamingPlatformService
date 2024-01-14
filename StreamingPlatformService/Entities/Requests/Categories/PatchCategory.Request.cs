using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace StreamingPlatformService.Entities.Requests.Categories;

public class PatchCategoryRequest
{
    [Required]
    [JsonPropertyName("id")]
    public Guid Id { get; set; }
    
    [Required]
    [JsonPropertyName("title")]
    public string Title { get; set; }
}