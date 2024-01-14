using System.Text.Json.Serialization;

namespace StreamingPlatformService.Entities.Responses.Categories;

public class PostCategoryResponse
{
    [JsonPropertyName("category")]
    public Category Category { get; set; }
}