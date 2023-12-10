using System.Net;
using AuthStreamingPlatformService.Entities.Api;
using AuthStreamingPlatformService.Infrastructure.ActionResults;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuthStreamingPlatformService.Infrastructure.Endpoints;

[Authorize]
[ApiController]
public abstract class EndpointBase : ControllerBase
{
    protected string Token => Request.Headers["Authorization"].ToString().Replace("Bearer ", string.Empty);

    [NonAction]
    public virtual ValidationErrorResult ValidationError(object? error)
    {
        return new ValidationErrorResult(error);
    }

    [NonAction]
    public virtual AuthorizationErrorResult AuthorizationError(object? error)
    {
        return new AuthorizationErrorResult(error);
    }

    [NonAction]
    public virtual ActionResult GetActionResult(ApiError? error, string defaultMessage = "")
    {
        if (error?.Code == HttpStatusCode.UnprocessableEntity)
            return ValidationError(error.Message);

        return BadRequest(error?.Message ?? defaultMessage);
    }
}
