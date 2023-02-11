using System.Net;
using System.Text.Json;
using FluentValidation;
using Recommendations.Application.Common.Exceptions;

namespace Recommendations.Web.Middlewares;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(RequestDelegate next, 
        ILogger<ExceptionHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
        }
        catch (AuthenticationException e)
        {
            await HandleExceptionAsync(httpContext, e, HttpStatusCode.Unauthorized);
        }
        catch (RecordIsExistException e)
        {
            await HandleExceptionAsync(httpContext, e, HttpStatusCode.Conflict);
        }
        catch (ValidationException e)
        {
            await HandleExceptionAsync(httpContext, e, HttpStatusCode.BadRequest);
        }
        catch (NotFoundException e)
        {
            await HandleExceptionAsync(httpContext, e, HttpStatusCode.NotFound);
        }
        catch (AccessDeniedException e)
        {
            await HandleExceptionAsync(httpContext, e, HttpStatusCode.Forbidden);
        }
        catch (Exception e)
        {
            await HandleExceptionAsync(httpContext, e, HttpStatusCode.InternalServerError);
        }
    }

    private async Task HandleExceptionAsync(HttpContext httpContext,
        Exception exception, HttpStatusCode statusCode)
    {
        httpContext.Response.ContentType = "application/json";
        httpContext.Response.StatusCode = (int)statusCode;

        var error = new
        {
            exception.Message,
            StatusCode = (int)statusCode,
        };
        var result = JsonSerializer.Serialize(error);
        _logger.LogError(exception, $"Error - {exception}");

        await httpContext.Response.WriteAsync(result);
    }
}
