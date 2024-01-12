using AuthStreamingPlatformService.Core.Abstractions.Services;
using AuthStreamingPlatformService.Entities;
using AuthStreamingPlatformService.Entities.Responses;
using AuthStreamingPlatformService.Infrastructure.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using TechDaily.Infrastructure.Endpoints;

namespace AuthStreamingPlatformService.Endpoints;

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