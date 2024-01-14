using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StreamingPlatformService.Core.Abstractions.Services;
using StreamingPlatformService.Core.Abstractions.Services.Users;
using StreamingPlatformService.Entities;
using StreamingPlatformService.Entities.Requests;
using StreamingPlatformService.Entities.Responses;
using StreamingPlatformService.Infrastructure.Endpoints;

namespace StreamingPlatformService.Endpoints.AuthEndpoints;

public class RegisterUser : EndpointBaseAsync.WithRequest<RegisterUserRequest>.WithActionResult<RegisterUserResponse>
{
    private readonly IUsersService _usersService;
    private readonly IClaimsService _claimsService;
    private readonly IJwtTokenService _jwtTokenService;

    public RegisterUser(IUsersService usersService, IClaimsService claimsService, IJwtTokenService jwtTokenService)
    {
        _usersService = usersService;
        _claimsService = claimsService;
        _jwtTokenService = jwtTokenService;
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

        var userClaims = await _claimsService.GetUserClaimsAsync(result.Value);
        if (userClaims.IsError)
            return GetActionResult(userClaims);

        var token = await _jwtTokenService.GetJwtToken(userClaims.Value.ToList());
        
        return Created(new RegisterUserResponse
        {
            Token = new JwtSecurityTokenHandler().WriteToken(token.Value), 
            Expiration = token.Value.ValidTo
        });
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