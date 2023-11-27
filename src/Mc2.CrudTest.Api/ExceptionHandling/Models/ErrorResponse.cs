namespace Mc2.CrudTest.Api.ExceptionHandling.Models;

public class ErrorResponse
{
    public ErrorResponse()
    {
        Errors = new List<ErrorDto>();
    }

    public List<ErrorDto> Errors { get; init; }
}