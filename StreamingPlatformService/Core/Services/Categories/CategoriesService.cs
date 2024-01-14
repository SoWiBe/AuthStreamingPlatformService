using MongoDB.Driver;
using StreamingPlatformService.Core.Abstractions.Errors;
using StreamingPlatformService.Core.Abstractions.Services.Categories;
using StreamingPlatformService.Core.Errors;
using StreamingPlatformService.Entities;
using StreamingPlatformService.Entities.Requests.Categories;
using StreamingPlatformService.Infrastructure.Data;

namespace StreamingPlatformService.Core.Services.Categories;

/// <summary>
/// Service for categories
/// </summary>
public class CategoriesService : ICategoriesService
{
    private readonly IAppMongoDbContext _mongoDbContext;
    private readonly IMongoCollection<Category> _categories;
    
    /// <summary>
    /// Ctor
    /// </summary>
    /// <param name="users"></param>
    /// <param name="mongoDbContext"></param>
    public CategoriesService(IAppMongoDbContext mongoDbContext)
    {
        _mongoDbContext = mongoDbContext;
        _categories = _mongoDbContext.GetDatabase().GetCollection<Category>("Categories");
    }
    
    /// <summary>
    /// Get all categories from mongo
    /// </summary>
    /// <returns></returns>
    public async Task<ErrorOr<IEnumerable<Category>>> GetAllCategories()
    {
        var categories = await _categories.Find(_ => true).ToListAsync();
        return categories ?? new List<Category>();
    }

    /// <summary>
    /// Get category by id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<ErrorOr<Category>> GetCategory(Guid id)
    {
        var filter = Builders<Category>.Filter.Eq("_id", id);

        var category = await _categories.Find(filter).FirstOrDefaultAsync();
        if (category is null)
            return Error.NotFound("Categories.Error", "Category is not found");

        return category;
    }

    /// <summary>
    /// Get category by title
    /// </summary>
    /// <param name="title"></param>
    /// <returns></returns>
    public async Task<ErrorOr<Category>> GetCategoryByTitle(string title)
    {
        var filter = Builders<Category>.Filter.Eq("title", title);

        var category = await _categories.Find(filter).FirstOrDefaultAsync();
        if (category is null)
            return Error.NotFound("Categories.Error", "Category is not found");

        return category;
    }

    /// <summary>
    /// Delete category by id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<IErrorOr> DeleteCategory(Guid id)
    {
        var filter = Builders<Category>.Filter.Eq("_id", id);
        await _categories.DeleteOneAsync(filter);

        return ErrorOr.NoError();
    }

    /// <summary>
    /// Delete category by title
    /// </summary>
    /// <param name="title"></param>
    /// <returns></returns>
    public async Task<IErrorOr> DeleteCategoryByTitle(string title)
    {
        var filter = Builders<Category>.Filter.Eq("title", title);
        await _categories.DeleteOneAsync(filter);

        return ErrorOr.NoError();
    }

    /// <summary>
    /// Post new category to mongo
    /// </summary>
    /// <param name="category"></param>
    /// <returns></returns>
    public async Task<ErrorOr<Category>> PostCategory(Category category)
    {
        var categories = await GetAllCategories();
        if (categories.IsError)
            return categories.FirstError;

        if (categories.Value.Any(x => x.Title.ToLower() == category.Title.ToLower()))
            return Error.Conflict("Categories.Conflict", "Category with this title already exist");
        
        await _categories.InsertOneAsync(category);

        return category;
    }

    /// <summary>
    /// Patch Category
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public async Task<ErrorOr<Category>> PatchCategory(PatchCategoryRequest request)
    {
        var filter = Builders<Category>.Filter.Eq("_id", request.Id);

        var update = Builders<Category>.Update.Set("title", request.Title);
        await UpdateField(filter, update);

        var category = await GetCategory(request.Id);
        if (category.IsError)
            return category.Errors.FirstOrDefault();

        return category.Value;
    }
    
    private async Task UpdateField(FilterDefinition<Category> filter, UpdateDefinition<Category> update)
    {
        await _categories.UpdateOneAsync(filter, update);
    }
}