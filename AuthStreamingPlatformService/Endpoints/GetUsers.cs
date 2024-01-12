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
    
    [AllowAnonymous]
    [ApiExplorerSettings(GroupName = "User")]
    [HttpGet("/users")]
    public override async Task<ActionResult<GetUsersResponse>> HandleAsync(CancellationToken cancellationToken = default)
    {
        var result = await _usersService.GetAllUsers();

        return Ok(new GetUsersResponse { Users = result.Value });
    }
}