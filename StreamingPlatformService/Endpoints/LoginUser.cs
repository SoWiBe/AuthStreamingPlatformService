using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StreamingPlatformService.Core.Abstractions.Services;
using StreamingPlatformService.Entities.Requests;
using StreamingPlatformService.Entities.Responses;
using StreamingPlatformService.Infrastructure.Endpoints;

namespace StreamingPlatformService.Endpoints;

public class LoginUser : EndpointBaseAsync.WithRequest<LoginUserRequest>.WithActionResult<LoginUserResponse>
{
    private readonly IUsersService _usersService;
    private readonly IClaimsService _claimsService;
    private readonly IJwtTokenService _jwtTokenService;

    public LoginUser(IUsersService usersService, IClaimsService claimsService, IJwtTokenService jwtTokenService)
    {
        _usersService = usersService;
        _claimsService = claimsService;
        _jwtTokenService = jwtTokenService;
    }
    
    [AllowAnonymous]
    [HttpPost("/auth/sign-in")]
    [ApiExplorerSettings(GroupName = "Auth")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public override async Task<ActionResult<LoginUserResponse>> HandleAsync(LoginUserRequest request,
        CancellationToken cancellationToken = default)
    {
        var user = await _usersService.GetUserByEmail(request.Email);
        if (user.IsError)
            return GetActionResult(user);

        var userClaims = await _claimsService.GetUserClaimsAsync(user.Value);
        if (userClaims.IsError)
            return GetActionResult(userClaims);

        var token = await _jwtTokenService.GetJwtToken(userClaims.Value.ToList());

        return Ok(new LoginUserResponse
            { 
                Token = new JwtSecurityTokenHandler().WriteToken(token.Value), 
                Expiration = token.Value.ValidTo
            }); 
    }
}