using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace StreamingPlatformService.Entities.Requests.Categories;

public class DeleteCategoryRequest
{
    public const string Route = "/category/delete-category/{id:guid}";
    
    [Required]
    [FromRoute(Name = "id")]
    public Guid Id { get; set; }
}