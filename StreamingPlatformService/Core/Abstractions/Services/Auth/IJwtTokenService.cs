using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using StreamingPlatformService.Core.Errors;

namespace StreamingPlatformService.Core.Abstractions.Services;

public interface IJwtTokenService
{
    Task<ErrorOr<JwtSecurityToken>> GetJwtToken(List<Claim> userClaims);
}