using AuthStreamingPlatformService.Core.Abstractions.Services;
using AuthStreamingPlatformService.Entities.Requests;
using AuthStreamingPlatformService.Entities.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TechDaily.Infrastructure.Endpoints;

namespace AuthStreamingPlatformService.Endpoints;

public class RegisterUser : EndpointBaseAsync.WithRequest<RegisterUserRequest>.WithActionResult<RegisterUserResponse>
{
    private readonly IUsersService _usersService;

    public RegisterUser(IUsersService usersService)
    {
        _usersService = usersService;
    }
    
    [AllowAnonymous]
    [ApiExplorerSettings(GroupName = "User")]
    [HttpPost("/user")]
    public override async Task<ActionResult<RegisterUserResponse>> HandleAsync(RegisterUserRequest request, 
        CancellationToken cancellationToken = default)
    {
        return Ok();
    }
}