using StreamingPlatformService.Core.Abstractions.Errors;
using StreamingPlatformService.Core.Errors;
using StreamingPlatformService.Entities;
using StreamingPlatformService.Entities.Requests;
using StreamingPlatformService.Entities.Requests.Categories;

namespace StreamingPlatformService.Core.Abstractions.Services.Categories;

public interface ICategoriesService
{
    Task<ErrorOr<IEnumerable<Category>>> GetAllCategories();
    Task<ErrorOr<Category>> GetCategory(Guid id);
    Task<ErrorOr<Category>> GetCategoryByTitle(string title);
    Task<IErrorOr> DeleteCategory(Guid id);
    Task<IErrorOr> DeleteCategoryByTitle(string title);
    Task<ErrorOr<Category>> PostCategory(Category category);
    Task<ErrorOr<Category>> PatchCategory(PatchCategoryRequest request);
}