using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace StreamingPlatformService.Infrastructure.ActionResults;

[DefaultStatusCode(DefaultStatusCode)]
public class InternalServerErrorResult : ObjectResult
{
    private const int DefaultStatusCode = StatusCodes.Status500InternalServerError;
    
    public InternalServerErrorResult(object? error)
        : base(error)
    {
        StatusCode = DefaultStatusCode;
    }

    public InternalServerErrorResult(ModelStateDictionary modelState)
        : base(new SerializableError(modelState))
    {
        if (modelState == null) throw new ArgumentNullException(nameof(modelState));

        StatusCode = DefaultStatusCode;
    }
}