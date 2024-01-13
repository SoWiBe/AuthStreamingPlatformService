using Microsoft.AspNetCore.Mvc;

namespace StreamingPlatformService.Infrastructure.ActionResults;

public class CreatedStatusResult : CreatedResult
{
    public CreatedStatusResult(object? value) : base("", value)
    {
    }
}