using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace AuthStreamingPlatformService.Infrastructure.ActionResults;

[DefaultStatusCode(DefaultStatusCode)]
public class FailureErrorResult : ObjectResult
{
    private const int DefaultStatusCode = 420;

    public FailureErrorResult(object? error)
        : base(error)
    {
        StatusCode = DefaultStatusCode;
    }

    public FailureErrorResult(ModelStateDictionary modelState)
        : base(new SerializableError(modelState))
    {
        if (modelState == null) throw new ArgumentNullException(nameof(modelState));

        StatusCode = DefaultStatusCode;
    }
}