using AuthStreamingPlatformService.Core.Errors;
using AuthStreamingPlatformService.Entities;

namespace AuthStreamingPlatformService.Core.Abstractions.Services;

public interface IUsersService
{
    Task<ErrorOr<IEnumerable<User>>> GetAllUsers();
    Task<ErrorOr<User>> GetUser(Guid id);
    Task<ErrorOr<User>> GetUserByEmail(string email);
    Task<ErrorOr<User>> PostUser(User user);
    Task<ErrorOr<User>> PatchUser(User user);
    Task<ErrorOr<User>> PutUser(User user);
    Task<ErrorOr<User>> DeleteUser(User user);
}