namespace StreamingPlatformService.Entities.Responses.Categories;

public class GetCategoriesResponse
{
    public IEnumerable<Category> Categories { get; set; }
}