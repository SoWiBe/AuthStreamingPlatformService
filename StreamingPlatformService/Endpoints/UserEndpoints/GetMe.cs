using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using StreamingPlatformService.Core.Abstractions.Errors;
using StreamingPlatformService.Core.Abstractions.Services;
using StreamingPlatformService.Core.Abstractions.Services.Users;
using StreamingPlatformService.Entities.Responses;
using StreamingPlatformService.Infrastructure.Endpoints;

namespace StreamingPlatformService.Endpoints.UserEndpoints;

public class GetMe : EndpointBaseAsync.WithoutRequest.WithActionResult<GetMeResponse>
{
    private readonly IUsersService _usersService;

    public GetMe(IUsersService usersService)
    {
        _usersService = usersService;
    }
    
    [HttpGet("/user/get-me")]
    [ApiExplorerSettings(GroupName = "User")]
    public override async Task<ActionResult<GetMeResponse>> HandleAsync(CancellationToken cancellationToken = default)
    {
        var rawId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;

        if (string.IsNullOrEmpty(rawId))
            return BadRequest(Error.Custom(5, "User.BadRequest", "User not found"));

        var user = await _usersService.GetUserByEmail(rawId);
        if (user.IsError)
            return GetActionResult(user);

        return Ok(new GetMeResponse { User = user.Value });
    }
}