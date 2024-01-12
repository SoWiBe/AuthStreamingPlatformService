using AuthStreamingPlatformService.Core.Abstractions.Errors;
using AuthStreamingPlatformService.Core.Errors;
using AuthStreamingPlatformService.Entities;
using AuthStreamingPlatformService.Entities.Requests;

namespace AuthStreamingPlatformService.Core.Abstractions.Services;

public interface IUsersService
{
    Task<ErrorOr<IEnumerable<User>>> GetAllUsers();
    Task<ErrorOr<User>> GetUser(Guid id);
    Task<ErrorOr<User>> GetUserByEmail(string email);
    Task<ErrorOr<User>> PostUser(User user);
    Task<ErrorOr<User>> PatchUser(string email, PatchUserRequest request);
    Task<IErrorOr> DeleteUser(string email);
}