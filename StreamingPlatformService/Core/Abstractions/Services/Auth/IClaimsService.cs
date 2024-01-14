using System.Security.Claims;
using StreamingPlatformService.Core.Errors;
using StreamingPlatformService.Entities;

namespace StreamingPlatformService.Core.Abstractions.Services;

public interface IClaimsService
{
    Task<ErrorOr<IEnumerable<Claim>>> GetUserClaimsAsync(User user);
}