using Microsoft.AspNetCore.Mvc;
using StreamingPlatformService.Core.Abstractions.Services.Categories;
using StreamingPlatformService.Entities.Requests.Categories;
using StreamingPlatformService.Entities.Responses.Categories;
using StreamingPlatformService.Infrastructure.Endpoints;

namespace StreamingPlatformService.Endpoints.CategoriesEndpoints;

/// <summary>
/// Patch category endpoint
/// </summary>
public class PatchCategory : EndpointBaseAsync.WithRequest<PatchCategoryRequest>.WithActionResult<PatchCategoryResponse>
{
    private readonly ICategoriesService _categoriesService;

    /// <summary>
    /// Ctor for endpoint
    /// </summary>
    /// <param name="categoriesService"></param>
    public PatchCategory(ICategoriesService categoriesService)
    {
        _categoriesService = categoriesService;
    }
    
    [HttpPatch("/category/patch-category")]
    [ApiExplorerSettings(GroupName = "Category")]
    public override async Task<ActionResult<PatchCategoryResponse>> HandleAsync(PatchCategoryRequest request, 
        CancellationToken cancellationToken = default)
    {
        var category = await _categoriesService.PatchCategory(request);

        return category.IsError ? 
            GetActionResult(category) : 
            Ok(new PatchCategoryResponse { Category = category.Value });
    }
}