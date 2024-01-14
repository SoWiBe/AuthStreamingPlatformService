using Microsoft.AspNetCore.Mvc;
using StreamingPlatformService.Core.Abstractions.Services.Categories;
using StreamingPlatformService.Entities;
using StreamingPlatformService.Entities.Requests.Categories;
using StreamingPlatformService.Entities.Responses.Categories;
using StreamingPlatformService.Infrastructure.Endpoints;

namespace StreamingPlatformService.Endpoints.CategoriesEndpoints;

/// <summary>
/// Post category endpoint
/// </summary>
public class PostCategory : EndpointBaseAsync.WithRequest<PostCategoryRequest>.WithActionResult<PostCategoryResponse>
{
    private readonly ICategoriesService _categoriesService;

    /// <summary>
    /// Ctor for endpoint
    /// </summary>
    /// <param name="categoriesService"></param>
    public PostCategory(ICategoriesService categoriesService)
    {
        _categoriesService = categoriesService;
    }
    
    [HttpPost("/category/post-category")]
    [ApiExplorerSettings(GroupName = "Category")]
    public override async Task<ActionResult<PostCategoryResponse>> HandleAsync(PostCategoryRequest request, 
        CancellationToken cancellationToken = default)
    {
        var category = new Category
        {
            Title = request.Title
        };

        var result = await _categoriesService.PostCategory(category);

        return result.IsError ? GetActionResult(result) : Ok(new PostCategoryResponse { Category = category});
    }
}