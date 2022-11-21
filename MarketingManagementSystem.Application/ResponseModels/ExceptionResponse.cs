using Newtonsoft.Json;

namespace MarketingManagementSystem.Application.ResponseModels;

public class ExceptionResponse
{
    [JsonProperty(PropertyName = "statusCode")]
    public int StatusCode { get; set; }

    [JsonProperty(PropertyName = "message")]
    public string Message { get; set; }
}
