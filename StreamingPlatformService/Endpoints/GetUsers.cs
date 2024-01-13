using Microsoft.AspNetCore.Mvc;
using StreamingPlatformService.Core.Abstractions.Services;
using StreamingPlatformService.Entities.Responses;
using StreamingPlatformService.Infrastructure.Endpoints;

namespace StreamingPlatformService.Endpoints;

public class GetUsers : EndpointBaseAsync.WithoutRequest.WithActionResult<GetUsersResponse>
{
    private readonly IUsersService _usersService;

    public GetUsers(IUsersService usersService)
    {
        _usersService = usersService;
    }
    
    [HttpGet("/user/users")]
    [ApiExplorerSettings(GroupName = "User")]
    public override async Task<ActionResult<GetUsersResponse>> HandleAsync(CancellationToken cancellationToken = default)
    {
        var result = await _usersService.GetAllUsers();
        if (result.IsError)
            return GetActionResult(result);

        return Ok(new GetUsersResponse { Users = result.Value });
    }
}