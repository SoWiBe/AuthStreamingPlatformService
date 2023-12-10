using System.Net;
using Newtonsoft.Json.Linq;

namespace TechDaily.Entities.Api;

public record ApiError
{
    private const string Detail = "detail";

    public ApiError(HttpStatusCode code, string message)
    {
        Code = code;
        Message = message;
    }

    public ApiError(HttpStatusCode code, JObject? message)
    {
        Code = code;
        Message = message?[Detail]?.ToObject<string>() ?? "";
    }

    public HttpStatusCode Code { get; }
    public string Message { get; }
}