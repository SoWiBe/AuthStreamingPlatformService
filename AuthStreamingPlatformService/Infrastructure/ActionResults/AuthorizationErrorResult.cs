using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace AuthStreamingPlatformService.Infrastructure.ActionResults;

[DefaultStatusCode(DefaultStatusCode)]
public class AuthorizationErrorResult : ObjectResult
{
    private const int DefaultStatusCode = StatusCodes.Status401Unauthorized;

    public AuthorizationErrorResult(object? error)
        : base(error)
    {
        StatusCode = DefaultStatusCode;
    }

    public AuthorizationErrorResult(ModelStateDictionary modelState)
        : base(new SerializableError(modelState))
    {
        if (modelState == null) throw new ArgumentNullException(nameof(modelState));

        StatusCode = DefaultStatusCode;
    }
}