using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace AuthStreamingPlatformService.Infrastructure.ActionResults;

[DefaultStatusCode(DefaultStatusCode)]
public class ValidationErrorResult : ObjectResult
{
    private const int DefaultStatusCode = StatusCodes.Status422UnprocessableEntity;

    public ValidationErrorResult(object? error)
        : base(error)
    {
        StatusCode = DefaultStatusCode;
    }

    public ValidationErrorResult(ModelStateDictionary modelState)
        : base(new SerializableError(modelState))
    {
        if (modelState == null) throw new ArgumentNullException(nameof(modelState));

        StatusCode = DefaultStatusCode;
    }
}
