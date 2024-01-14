using System.Text.Json.Serialization;

namespace StreamingPlatformService.Entities.Responses.Categories;

public class PatchCategoryResponse
{
    [JsonPropertyName("category")]
    public Category Category { get; set; }
}