using Microsoft.AspNetCore.Mvc;

namespace AuthStreamingPlatformService.Infrastructure.ActionResults;

public class CreatedStatusResult : CreatedResult
{
    public CreatedStatusResult(object? value) : base("", value)
    {
    }
}