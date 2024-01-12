﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace AuthStreamingPlatformService.Infrastructure.ActionResults;


[DefaultStatusCode(DefaultStatusCode)]
public class ForbiddenErrorResult : ObjectResult
{
    private const int DefaultStatusCode = StatusCodes.Status403Forbidden;

    public ForbiddenErrorResult(object? error)
        : base(error)
    {
        StatusCode = DefaultStatusCode;
    }

    public ForbiddenErrorResult(ModelStateDictionary modelState)
        : base(new SerializableError(modelState))
    {
        if (modelState == null) throw new ArgumentNullException(nameof(modelState));

        StatusCode = DefaultStatusCode;
    }
}