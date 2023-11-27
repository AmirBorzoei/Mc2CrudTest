using System.Net;
using Mc2.CrudTest.Api.ExceptionHandling.Models;
using Mc2.Framework.Core.Exception;

namespace Mc2.CrudTest.Api.ExceptionHandling.Policies;

public class DomainExceptionPolicy : HttpFaultProvider
{
    public override bool IsEligible(Exception ex)
    {
        return ex is DomainException;
    }

    protected override int GetStatusCode(Exception ex)
    {
        return (int)HttpStatusCode.UnprocessableEntity;
    }

    protected override ErrorResponse GetErrorResponse(Exception ex)
    {
        DomainException domainException = (DomainException)ex;
        ErrorResponse errorResponse = new();
        errorResponse.Errors.Add(new ErrorDto(domainException.ErrorCode, domainException.Message));
        return errorResponse;
    }
}