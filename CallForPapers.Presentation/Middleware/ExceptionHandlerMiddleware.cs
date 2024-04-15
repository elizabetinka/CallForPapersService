using System.Net;
using CallForPapers.Services;

namespace WebApplication1.Middleware;

public class ExceptionHandlerMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlerMiddleware> _logger;

    public ExceptionHandlerMiddleware(RequestDelegate next, ILogger<ExceptionHandlerMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task Invoke(HttpContext content)
    {
        try
        {
            await _next(content);
        }
        catch (Exception e)
        {
            await HandleExceptionAsync(content, e);
        }
    }

    private Task HandleExceptionAsync(HttpContext content, Exception exception)
    {
        var code = HttpStatusCode.InternalServerError;
        string message = exception.Message;
        switch (exception)
        {
            case ArgumentException ex:
                code = HttpStatusCode.BadRequest;
                break;
            case CallForPaperBackendException ex:
                code = HttpStatusCode.UnprocessableEntity;
                break;
            default:
                code = HttpStatusCode.InternalServerError;
                message = string.Empty;
                _logger.LogError(exception.Message);
                break;
        }

        content.Response.ContentType = "text/plain";
        content.Response.StatusCode = (int)code;
        return content.Response.WriteAsync(message);
    }
}