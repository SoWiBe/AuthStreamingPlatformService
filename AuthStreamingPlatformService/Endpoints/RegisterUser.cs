using AuthStreamingPlatformService.Core.Abstractions.Services;
using AuthStreamingPlatformService.Entities;
using AuthStreamingPlatformService.Entities.Constants;
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
    [HttpPost("/auth/sign-up")]
    [ApiExplorerSettings(GroupName = "Auth")]
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

        var result = await _usersService.PostUser(user);
        if (result.IsError)
            return GetActionResult(result);

        await SeedRoles();

        return Created(new RegisterUserResponse { Detail = "Success!" });
    }

    private async Task SeedRoles()
    {
        // if (!await _roleManager.RoleExistsAsync(UserRoles.Admin))
        //     await _roleManager.CreateAsync(new Role(UserRoles.User));
        //
        // if (!await _roleManager.RoleExistsAsync(UserRoles.User))
        //     await _roleManager.CreateAsync(new Role(UserRoles.User));
    }

}