using System.Security.Claims;
using AuthStreamingPlatformService.Core.Abstractions.Errors;
using AuthStreamingPlatformService.Core.Abstractions.Services;
using AuthStreamingPlatformService.Entities.Requests;
using AuthStreamingPlatformService.Entities.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TechDaily.Infrastructure.Endpoints;

namespace AuthStreamingPlatformService.Endpoints;

public class PatchUser : EndpointBaseAsync.WithRequest<PatchUserRequest>.WithActionResult<PatchUserResponse>
{
    private readonly IUsersService _usersService;

    public PatchUser(IUsersService usersService)
    {
        _usersService = usersService;
    }
    
    [HttpPatch("/user/update-user")]
    [ApiExplorerSettings(GroupName = "User")]
    public override async Task<ActionResult<PatchUserResponse>> HandleAsync(PatchUserRequest request, 
        CancellationToken cancellationToken = default)
    {
        var email = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;

        if (string.IsNullOrEmpty(email))
            return BadRequest(Error.Custom(5, "User.BadRequest", "User not found"));
        
        var user = await _usersService.PatchUser(email, request);

        return user.IsError ? GetActionResult(user) : Ok(new PatchUserResponse { User = user.Value});
    }
}