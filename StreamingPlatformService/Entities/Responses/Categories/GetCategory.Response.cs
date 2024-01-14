using System.Text.Json.Serialization;

namespace StreamingPlatformService.Entities.Responses.Categories;

public class GetCategoryResponse
{
    [JsonPropertyName("category")]
    public Category Category { get; set; }
}