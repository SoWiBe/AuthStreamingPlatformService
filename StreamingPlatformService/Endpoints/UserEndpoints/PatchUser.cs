using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using StreamingPlatformService.Core.Abstractions.Errors;
using StreamingPlatformService.Core.Abstractions.Services;
using StreamingPlatformService.Entities.Requests;
using StreamingPlatformService.Entities.Responses;
using StreamingPlatformService.Infrastructure.Endpoints;

namespace StreamingPlatformService.Endpoints.UserEndpoints;

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