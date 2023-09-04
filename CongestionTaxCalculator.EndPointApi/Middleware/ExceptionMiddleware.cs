using System.Net;
using CongestionTaxCalculator.Shared.Exceptions;
using CongestionTaxCalculator.Shared.ResultObject;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace CongestionTaxCalculator.EndPointApi.Middleware;

public class ExceptionMiddleware
{
    private readonly IWebHostEnvironment _env;
    private readonly ILogger<ExceptionMiddleware> _logger;
    private readonly RequestDelegate _next;

    public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger, IWebHostEnvironment env)
    {
        _logger = logger;
        _env = env;
        _next = next;
    }

    public async Task InvokeAsync(HttpContext httpContext)
    {
        var message = "پیام مورد نظر یافت نشد.";
        var apiStatusCode = ApiResultStatusCode.ServerError;

        try
        {
            await _next(httpContext);
        }
        catch (BaseException exception)
        {
            _logger.LogError(exception, "خطایی کنترل شده ای رخ داده");
            message = exception.Message;
            await WriteToResponseAsync();
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "خطایی نامشخصی رخ داده");
            message = exception.InnerException?.Message ?? exception.Message;
            await WriteToResponseAsync();
        }
        
        async Task WriteToResponseAsync()
        {
            if (httpContext.Response.HasStarted)
                throw new InvalidOperationException(
                    "The response has already started, the http status code middleware will not be executed.");

            var result = new ApiResult(false, apiStatusCode, message);

            var json = JsonConvert.SerializeObject(result, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            });

            httpContext.Response.StatusCode = (int)HttpStatusCode.OK;
            httpContext.Response.ContentType = "application/json";
            await httpContext.Response.WriteAsync(json);
        }
    }
}