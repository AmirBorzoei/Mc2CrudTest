using System.Net;
using Mc2.CrudTest.Api.ExceptionHandling.Models;
using Mc2.Framework.Core.Exception;

namespace Mc2.CrudTest.Api.ExceptionHandling.Policies;

public class NotFoundExceptionPolicy : HttpFaultProvider
{
    public override bool IsEligible(Exception ex)
    {
        return ex is NotFoundException;
    }

    protected override int GetStatusCode(Exception ex)
    {
        return (int)HttpStatusCode.NotFound;
    }

    protected override ErrorResponse GetErrorResponse(Exception ex)
    {
        NotFoundException notFoundException = (NotFoundException)ex;
        ErrorResponse errorResponse = new();
        errorResponse.Errors.Add(new ErrorDto((int)HttpStatusCode.NotFound, notFoundException.Message));
        return errorResponse;
    }
}