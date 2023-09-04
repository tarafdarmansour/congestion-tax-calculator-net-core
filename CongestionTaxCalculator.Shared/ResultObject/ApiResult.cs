using System.Linq;
using CongestionTaxCalculator.Utilities;
using Newtonsoft.Json;

namespace CongestionTaxCalculator.Shared.ResultObject;

public class ApiResult
{
    private readonly bool _isSuccess;

    public ApiResult(bool isSuccess, ApiResultStatusCode statusCode, string? message = null)
    {
        _isSuccess = isSuccess;
        StatusCode = statusCode;
        Message = message ?? statusCode.ToDisplay();
    }

    public bool IsSuccess => _isSuccess && StatusCode == ApiResultStatusCode.Success;

    public ApiResultStatusCode StatusCode { get; set; }

    [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
    public string Message { get; set; }
}

public class ApiResult<TData> : ApiResult where TData : class
{
    public ApiResult(bool isSuccess, ApiResultStatusCode statusCode, TData data, string message = null)
        : base(isSuccess, statusCode, message)
    {
        Data = data;
    }

    [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
    public TData Data { get; set; }

}