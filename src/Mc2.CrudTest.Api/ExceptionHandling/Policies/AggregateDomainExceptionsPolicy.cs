using System.Net;
using Mc2.CrudTest.Api.ExceptionHandling.Models;
using Mc2.Framework.Core.Exception;

namespace Mc2.CrudTest.Api.ExceptionHandling.Policies;

public class AggregateDomainExceptionsPolicy : HttpFaultProvider
{
    public override bool IsEligible(Exception ex)
    {
        return ex is AggregateDomainExceptions;
    }

    protected override int GetStatusCode(Exception ex)
    {
        return (int)HttpStatusCode.UnprocessableEntity;
    }

    protected override ErrorResponse GetErrorResponse(Exception ex)
    {
        AggregateDomainExceptions aggregateDomainExceptions = (AggregateDomainExceptions)ex;

        ErrorResponse errorResponse = new();
        List<ErrorDto> errorDtos = aggregateDomainExceptions.DomainExceptions
            .Select(e => new ErrorDto(e.ErrorCode, e.Message)).ToList();
        errorResponse.Errors.AddRange(errorDtos);

        return errorResponse;
    }
}