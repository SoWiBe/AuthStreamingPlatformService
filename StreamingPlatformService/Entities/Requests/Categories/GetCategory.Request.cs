using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace StreamingPlatformService.Entities.Requests.Categories;

public class GetCategoryRequest
{
    public const string Route = "/category/get-category/{id:guid}";
    
    [Required] [FromRoute(Name = "id")] public Guid Id { get; set; }
}