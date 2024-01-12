using System.Security.Claims;
using AuthStreamingPlatformService.Core.Errors;
using AuthStreamingPlatformService.Entities;

namespace AuthStreamingPlatformService.Core.Abstractions.Services;

public interface IClaimsService
{
    Task<ErrorOr<IEnumerable<Claim>>> GetUserClaimsAsync(User user);
}