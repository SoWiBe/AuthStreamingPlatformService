using AuthStreamingPlatformService.Core.Abstractions.Errors;
using AuthStreamingPlatformService.Core.Abstractions.Services;
using AuthStreamingPlatformService.Core.Errors;
using AuthStreamingPlatformService.Entities;
using AuthStreamingPlatformService.Infrastructure.Data;
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

    public async Task<ErrorOr<User>> PostUser(User user)
    {
        await _users.InsertOneAsync(user);

        return user;
    }

    public async Task<ErrorOr<User>> PatchUser(User user)
    {
        throw new NotImplementedException();
    }

    public Task<ErrorOr<User>> PutUser(User user)
    {
        throw new NotImplementedException();
    }

    public Task<ErrorOr<User>> DeleteUser(User user)
    {
        throw new NotImplementedException();
    }
}