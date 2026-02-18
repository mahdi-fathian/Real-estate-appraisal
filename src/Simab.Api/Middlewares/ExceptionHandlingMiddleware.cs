using System.Net;
using System.Text.Json;
using Simab.Domain.Exceptions;

namespace Simab.Api.Middlewares;

/// <summary>
/// Middleware for handling exceptions and returning standardized error responses
/// </summary>
public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;
    
    public ExceptionHandlingMiddleware(
        RequestDelegate next,
        ILogger<ExceptionHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }
    
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An unhandled exception occurred");
            await HandleExceptionAsync(context, ex);
        }
    }
    
    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var code = HttpStatusCode.InternalServerError;
        var message = "An error occurred while processing your request.";
        var errorCode = "internal_error";
        
        switch (exception)
        {
            case DomainException domainEx:
                code = HttpStatusCode.UnprocessableEntity;
                message = domainEx.Message;
                errorCode = "domain_error";
                break;
                
            case InvalidOperationException:
                code = HttpStatusCode.BadRequest;
                message = exception.Message;
                errorCode = "business_error";
                break;
                
            case ArgumentNullException:
            case ArgumentException:
                code = HttpStatusCode.BadRequest;
                message = exception.Message;
                errorCode = "validation_error";
                break;
        }
        
        var response = new
        {
            code = errorCode,
            message = message,
            traceId = context.TraceIdentifier,
            timestamp = DateTime.UtcNow
        };
        
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)code;
        
        var json = JsonSerializer.Serialize(response, new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        });
        
        return context.Response.WriteAsync(json);
    }
}
