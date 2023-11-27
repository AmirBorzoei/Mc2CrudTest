using System.Text.Json;
using System.Text.Json.Serialization;
using Mc2.CrudTest.Api.ExceptionHandling.Models;

namespace Mc2.CrudTest.Api.ExceptionHandling.Policies;

public abstract class HttpFaultProvider : IExceptionPolicy
{
    public virtual int Order => 0;

    public abstract bool IsEligible(Exception ex);

    public async void Apply(object context, Exception ex)
    {
        await PrepareResponse(context, ex);
    }

    protected abstract int GetStatusCode(Exception ex);

    protected abstract ErrorResponse GetErrorResponse(Exception ex);

    private async Task PrepareResponse(object context, Exception ex)
    {
        if (context is HttpContext httpContext)
        {
            httpContext.Response.Clear();
            httpContext.Response.StatusCode = GetStatusCode(ex);
            httpContext.Response.ContentType = "application/json";
            
            string response = JsonSerializer.Serialize(GetErrorResponse(ex));

            await httpContext.Response.WriteAsync(response);
        }
    }
}