using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using StreamingPlatformService.Core.Abstractions.Errors;
using StreamingPlatformService.Core.Abstractions.Services;
using StreamingPlatformService.Entities.Responses;
using StreamingPlatformService.Infrastructure.Endpoints;

namespace StreamingPlatformService.Endpoints;

public class DeleteUser : EndpointBaseAsync.WithoutRequest.WithActionResult<DeleteUserResponse>
{
    private readonly IUsersService _usersService;

    public DeleteUser(IUsersService usersService)
    {
        _usersService = usersService;
    }
    
    [HttpDelete("/user/delete-user")]
    [ApiExplorerSettings(GroupName = "User")]
    public override async Task<ActionResult<DeleteUserResponse>> HandleAsync(CancellationToken cancellationToken = default)
    {
        var email = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;

        if (string.IsNullOrEmpty(email))
            return BadRequest(Error.Custom(5, "User.BadRequest", "User not found"));
        
        var result = await _usersService.DeleteUser(email);
        if (result.IsError)
            return GetActionResult(result);

        return Ok(new DeleteUserResponse { Detail = "Success!" });
    }
}