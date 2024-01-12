using System.Net;
using AuthStreamingPlatformService.Core.Abstractions.Errors;
using AuthStreamingPlatformService.Entities.Api;
using AuthStreamingPlatformService.Entities.Enums;
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
    public virtual FailureErrorResult FailureError(object? error)
    {
        return new FailureErrorResult(error);
    }

    [NonAction]
    public virtual InternalServerErrorResult InternalServerError(object? error)
    {
        return new InternalServerErrorResult(error);
    }
    
    [NonAction]
    public virtual ForbiddenErrorResult ForbiddenError(object? error)
    {
        return new ForbiddenErrorResult(error);
    }

    [NonAction]
    public virtual AuthorizationErrorResult AuthorizationError(object? error)
    {
        return new AuthorizationErrorResult(error);
    }
    
    [NonAction]
    public virtual CreatedStatusResult Created(object? value)
    {
        return new CreatedStatusResult(value);
    }
    
    [NonAction]
    public virtual ActionResult GetActionResult(IErrorOr entity)
    {
        var error = entity.Errors.First();
        
        return error.Type switch
        {
            ErrorType.BadRequest => BadRequest(error),
            ErrorType.Failure => FailureError(error),
            ErrorType.Unexpected => InternalServerError(error),
            ErrorType.Validation => ValidationError(error),
            ErrorType.Conflict => Conflict(error),
            ErrorType.NotFound => NotFound(error),
            ErrorType.Unauthorized => Unauthorized(error),
            ErrorType.UnprocessableContent => UnprocessableEntity(error),
            ErrorType.Forbidden => ForbiddenError(error)
        };
    }
}
