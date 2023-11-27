using System.Net;
using Mc2.CrudTest.Api.ExceptionHandling.Models;
using Mc2.Framework.Core.Exception;

namespace Mc2.CrudTest.Api.ExceptionHandling.Policies;

public class UnhandledExceptionPolicy : HttpFaultProvider
{
    public override int Order => 999;

    public override bool IsEligible(Exception ex)
    {
        return true;
    }

    protected override int GetStatusCode(Exception ex)
    {
        return (int)HttpStatusCode.InternalServerError;
    }

    protected override ErrorResponse GetErrorResponse(Exception ex)
    {
        string? aspNetCoreEnvironment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
        string errorMessage = aspNetCoreEnvironment == Environments.Development ?
            ex.Message :
            "Server Error!!!";

        ErrorResponse errorResponse = new();
        errorResponse.Errors.Add(new ErrorDto((int)HttpStatusCode.InternalServerError, errorMessage));

        return errorResponse;
    }
}