using StreamingPlatformService.Core.Abstractions.Errors;
using StreamingPlatformService.Core.Errors;
using StreamingPlatformService.Entities;
using StreamingPlatformService.Entities.Requests;

namespace StreamingPlatformService.Core.Abstractions.Services.Users;

public interface IUsersService
{
    Task<ErrorOr<IEnumerable<User>>> GetAllUsers();
    Task<ErrorOr<User>> GetUser(Guid id);
    Task<ErrorOr<User>> GetUserByEmail(string email);
    Task<ErrorOr<User>> PostUser(User user);
    Task<ErrorOr<User>> PatchUser(string email, PatchUserRequest request);
    Task<IErrorOr> UpdatePassword(PatchPasswordRequest request);
    Task<IErrorOr> DeleteUser(Guid id);
}