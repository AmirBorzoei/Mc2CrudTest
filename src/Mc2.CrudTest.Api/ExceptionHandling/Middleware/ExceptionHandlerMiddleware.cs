using Mc2.CrudTest.Api.ExceptionHandling.Handler;
using Microsoft.AspNetCore.Mvc;

namespace Mc2.CrudTest.Api.ExceptionHandling.Middleware;

public class ExceptionHandlerMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionHandlerMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext httpContext, [FromServices] IExceptionHandler exceptionHandler)
    {
        try
        {
            await _next(httpContext);
        }
        catch (Exception ex)
        {
            exceptionHandler.Handle(httpContext, ex);
        }
    }
}