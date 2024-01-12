using AuthStreamingPlatformService.Core.Abstractions.Errors;
using AuthStreamingPlatformService.Core.Abstractions.Services;
using AuthStreamingPlatformService.Core.Errors;
using AuthStreamingPlatformService.Entities;
using AuthStreamingPlatformService.Entities.Requests;
using AuthStreamingPlatformService.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using MongoDB.Bson;
using MongoDB.Driver;

namespace AuthStreamingPlatformService.Core.Services;

public class UsersService : IUsersService
{
    private readonly IAppMongoDbContext _mongoDbContext;
    private readonly IMongoCollection<User> _users;

    public UsersService(IAppMongoDbContext mongoDbContext)
    {
        _mongoDbContext = mongoDbContext;
        _users = _mongoDbContext.GetDatabase().GetCollection<User>("Users");
    }

    public async Task<ErrorOr<IEnumerable<User>>> GetAllUsers()
    {
        var users = await _users.Find(_ => true).ToListAsync();
        return users ?? new List<User>();
    }

    public async Task<ErrorOr<User>> GetUser(Guid id)
    {
        var filter = Builders<User>.Filter.Eq("_id", id);

        var user = await _users.Find(filter).FirstOrDefaultAsync();
        if (user is null)
            return Error.NotFound("Users.Error", "User not found");

        return user;
    }

    public async Task<ErrorOr<User>> GetUserByEmail(string email)
    {
        var filter = Builders<User>.Filter.Eq("email", email);
        
        var user = await _users.Find(filter).FirstOrDefaultAsync();
        if (user is null)
            return Error.NotFound("Users.Error", "User not found");

        return user;
    }

    public async Task<ErrorOr<User>> PostUser(User user)
    {
        await _users.InsertOneAsync(user);

        return user;
    }

    public async Task<ErrorOr<User>> PatchUser(string email, PatchUserRequest request)
    {
        var filter = Builders<User>.Filter.Eq("email", email);

        UpdateDefinition<User>? update;
        
        if (request.FirstName is not null)
        {
            update = Builders<User>.Update.Set("first_name", request.FirstName);
            await UpdateField(filter, update);
        } 
        if (request.LastName is not null)
        {
            update = Builders<User>.Update.Set("last_name", request.FirstName);
            await UpdateField(filter, update);
        }
        if (request.Nick is not null)
        {
            update = Builders<User>.Update.Set("nick", request.Nick);
            await UpdateField(filter, update);
        }
        if (request.Logo is not null)
        {
            update = Builders<User>.Update.Set("logo", request.Logo);
            await UpdateField(filter, update);
        }

        var user = await GetUserByEmail(email);
        if (user.IsError)
            return user.Errors.FirstOrDefault();

        return user.Value;
    }

    public async Task<IErrorOr> DeleteUser(string email)
    {
        var filter = Builders<User>.Filter.Eq("email", email);
        
        await _users.DeleteOneAsync(filter);

        return ErrorOr.NoError();
    }

    private async Task UpdateField(FilterDefinition<User> filter, UpdateDefinition<User> update)
    {
        await _users.UpdateOneAsync(filter, update);
    }
}