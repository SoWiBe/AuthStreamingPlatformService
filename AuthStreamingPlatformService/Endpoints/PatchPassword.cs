using AuthStreamingPlatformService.Core.Abstractions.Services;
using AuthStreamingPlatformService.Entities.Requests;
using AuthStreamingPlatformService.Entities.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TechDaily.Infrastructure.Endpoints;

namespace AuthStreamingPlatformService.Endpoints;

public class PatchPassword : EndpointBaseAsync.WithRequest<PatchPasswordRequest>.WithActionResult<PatchPasswordResponse>
{
    private readonly IUsersService _usersService;

    public PatchPassword(IUsersService usersService)
    {
        _usersService = usersService;
    }
    
    [AllowAnonymous]
    [HttpPatch("/user/update-password")]
    [ApiExplorerSettings(GroupName = "User")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public override async Task<ActionResult<PatchPasswordResponse>> HandleAsync(PatchPasswordRequest request, 
        CancellationToken cancellationToken = default)
    {
        var result = await _usersService.UpdatePassword(request);

        return result.IsError ? GetActionResult(result) : Ok(new PatchPasswordResponse { Detail = "Success!"});
    }
}