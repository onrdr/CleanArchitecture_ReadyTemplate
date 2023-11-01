using Microsoft.AspNetCore.Diagnostics;
using System.Net.Mime;
using System.Net;

namespace WebUI.Middlewares;

public class GlobalExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    public GlobalExceptionHandlingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context, ILogger<GlobalExceptionHandlingMiddleware> logger)
    {
        try
        {
            await _next(context);
        }

        catch
        {
            var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
            if (contextFeature != null)
            {
                logger.LogError(contextFeature.Error.Message);
            }

            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            context.Response.ContentType = MediaTypeNames.Application.Json;
        }
    }
}
