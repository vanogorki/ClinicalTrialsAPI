using System.Net;
using ClinicalTrials.Application.Common;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace ClinicalTrials.API.Middlewares;

internal sealed class ExceptionMiddleware(IHttpContextAccessor accessor, ILogger<ExceptionMiddleware> logger)
    : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next.Invoke(context);
        }
        catch (Exception ex)
        {
            var statusCode = ex switch
            {
                ArgumentException or InvalidOperationException or ArgumentNullException => HttpStatusCode.BadRequest,
                KeyNotFoundException => HttpStatusCode.NotFound,
                _ => HttpStatusCode.InternalServerError
            };
            var exceptionMessage = ex is ArgumentException or InvalidOperationException or ArgumentNullException
                ? "An error occurred while processing your request."
                : ex.Message;
            var traceIdentifier = accessor.HttpContext?.TraceIdentifier;
            logger.LogError(ex, $"{exceptionMessage} Trace Identifier: {traceIdentifier}.");

            await HandleExceptionAsync(context, exceptionMessage, statusCode, traceIdentifier);
        }
    }

    //Global exception handler
    private static Task HandleExceptionAsync(HttpContext context, string exceptionMessage,
        HttpStatusCode statusCode, string? traceIdentifier = null)
    {
        var response = JsonConvert.SerializeObject(new BaseApiResponse
        {
            IsSuccess = false,
            Message = exceptionMessage,
            TraceIdentifier = traceIdentifier
        }, new JsonSerializerSettings
        {
            ContractResolver = new DefaultContractResolver
            {
                NamingStrategy = new CamelCaseNamingStrategy()
            },
            Formatting = Formatting.Indented
        });

        // Check if the response has already started
        if (!context.Response.HasStarted)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)statusCode;
        }

        return context.Response.WriteAsync(response);
    }
}