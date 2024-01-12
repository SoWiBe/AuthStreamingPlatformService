using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using AuthStreamingPlatformService.Core.Errors;

namespace AuthStreamingPlatformService.Core.Abstractions.Services;

public interface IJwtTokenService
{
    Task<ErrorOr<JwtSecurityToken>> GetJwtToken(List<Claim> userClaims);
}