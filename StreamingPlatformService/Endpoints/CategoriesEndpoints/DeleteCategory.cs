using Microsoft.AspNetCore.Mvc;
using StreamingPlatformService.Core.Abstractions.Services.Categories;
using StreamingPlatformService.Entities.Requests.Categories;
using StreamingPlatformService.Entities.Responses.Categories;
using StreamingPlatformService.Infrastructure.Endpoints;

namespace StreamingPlatformService.Endpoints.CategoriesEndpoints;

/// <summary>
/// Delete category endpoint
/// </summary>
public class DeleteCategory : EndpointBaseAsync.WithRequest<DeleteCategoryRequest>.WithActionResult<DeleteCategoryResponse>
{
    private readonly ICategoriesService _categoriesService;

    /// <summary>
    /// Ctor for endpoint
    /// </summary>
    /// <param name="categoriesService"></param>
    public DeleteCategory(ICategoriesService categoriesService)
    {
        _categoriesService = categoriesService;
    }
    
    [HttpDelete(DeleteCategoryRequest.Route)]
    [ApiExplorerSettings(GroupName = "Category")]
    public override async Task<ActionResult<DeleteCategoryResponse>> HandleAsync([FromRoute] DeleteCategoryRequest request, 
        CancellationToken cancellationToken = default)
    {
        var result = await _categoriesService.DeleteCategory(request.Id);

        return result.IsError ? GetActionResult(result) : Ok(new DeleteCategoryResponse { Detail = "Success!"});
    }
}