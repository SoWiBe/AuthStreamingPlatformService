using System.Security.Claims;
using AuthStreamingPlatformService.Core.Abstractions.Services;
using AuthStreamingPlatformService.Core.Errors;
using AuthStreamingPlatformService.Entities;

namespace AuthStreamingPlatformService.Core.Services;

public class ClaimsService : IClaimsService
{
    private readonly IUsersService _usersService;

    public ClaimsService(IUsersService usersService)
    {
        _usersService = usersService;
    }
    
    public async Task<ErrorOr<IEnumerable<Claim>>> GetUserClaimsAsync(User user)
    {
        List<Claim> userClaims = new()
        {
            new Claim(ClaimTypes.Name, user.Email),
            new Claim(ClaimTypes.Email, user.Email)
        };

        // var userRoles = await _usersService.GetUserRoles();
        //
        // foreach (var userRole in userRoles)
        // {
        //     userClaims.Add(new Claim(ClaimTypes.Role, userRole));
        // }
        
        return userClaims;
    }
}