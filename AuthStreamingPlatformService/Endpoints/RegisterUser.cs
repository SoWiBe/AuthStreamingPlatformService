using AuthStreamingPlatformService.Core.Abstractions.Services;
using AuthStreamingPlatformService.Entities;
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
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public override async Task<ActionResult<RegisterUserResponse>> HandleAsync(RegisterUserRequest request, 
        CancellationToken cancellationToken = default)
    {
        var user = new User
        {
            Nick = request.Nick,
            FirstName = request.FirstName,
            LastName = request.LastName,
            Email = request.Email,
            Password = request.Password,
            Logo = request.Logo
        };
        
        return Ok(new RegisterUserResponse { Detail = "Success!"} );
    }
}