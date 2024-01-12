﻿using System.Security.Claims;
using AuthStreamingPlatformService.Core.Abstractions.Errors;
using AuthStreamingPlatformService.Core.Abstractions.Services;
using AuthStreamingPlatformService.Entities.Responses;
using Microsoft.AspNetCore.Mvc;
using TechDaily.Infrastructure.Endpoints;

namespace AuthStreamingPlatformService.Endpoints;

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