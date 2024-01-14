using Microsoft.AspNetCore.Mvc;
using StreamingPlatformService.Core.Abstractions.Services.Categories;
using StreamingPlatformService.Entities.Responses.Categories;
using StreamingPlatformService.Infrastructure.Endpoints;

namespace StreamingPlatformService.Endpoints.CategoriesEndpoints;

/// <summary>
/// Get Categories Endpoint
/// </summary>
public class GetCategories : EndpointBaseAsync.WithoutRequest.WithActionResult<GetCategoriesResponse>
{
    private readonly ICategoriesService _categoriesService;

    /// <summary>
    /// Ctor for endpoint
    /// </summary>
    /// <param name="categoriesService"></param>
    public GetCategories(ICategoriesService categoriesService)
    {
        _categoriesService = categoriesService;
    }
    
    [HttpGet("/category/categories")]
    [ApiExplorerSettings(GroupName = "Category")]
    public override async Task<ActionResult<GetCategoriesResponse>> HandleAsync(CancellationToken cancellationToken = default)
    {
        var categories = await _categoriesService.GetAllCategories();

        return categories.IsError ? 
            GetActionResult(categories) : 
            Ok(new GetCategoriesResponse { Categories = categories.Value });
    }
}