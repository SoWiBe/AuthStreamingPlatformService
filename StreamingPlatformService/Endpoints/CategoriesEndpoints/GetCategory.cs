using Microsoft.AspNetCore.Mvc;
using StreamingPlatformService.Core.Abstractions.Services.Categories;
using StreamingPlatformService.Entities.Requests.Categories;
using StreamingPlatformService.Entities.Responses.Categories;
using StreamingPlatformService.Infrastructure.Endpoints;

namespace StreamingPlatformService.Endpoints.CategoriesEndpoints;

/// <summary>
/// Get category endpoint
/// </summary>
public class GetCategory : EndpointBaseAsync.WithRequest<GetCategoryRequest>.WithActionResult<GetCategoryResponse>
{
    private readonly ICategoriesService _categoriesService;

    /// <summary>
    /// Ctor endpoint
    /// </summary>
    /// <param name="categoriesService"></param>
    public GetCategory(ICategoriesService categoriesService)
    {
        _categoriesService = categoriesService;
    }
    
    [HttpGet(GetCategoryRequest.Route)]
    [ApiExplorerSettings(GroupName = "Category")]
    public override async Task<ActionResult<GetCategoryResponse>> HandleAsync([FromRoute]GetCategoryRequest request,
        CancellationToken cancellationToken = default)
    {
        var category = await _categoriesService.GetCategory(request.Id);

        return category.IsError ? 
            GetActionResult(category) : 
            Ok(new GetCategoryResponse { Category = category.Value });
    }
}