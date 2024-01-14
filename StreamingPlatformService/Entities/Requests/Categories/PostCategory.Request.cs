using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace StreamingPlatformService.Entities.Requests.Categories;

public class PostCategoryRequest
{
    [Required] [JsonPropertyName("title")] public string Title { get; set; }
}