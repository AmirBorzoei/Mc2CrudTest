namespace Mc2.CrudTest.Api.ExceptionHandling.Models;

public class ErrorDto
{
    public ErrorDto()
    {
    }

    public ErrorDto(int code, string message)
    {
        Code = code;
        Message = message;
    }

    public int? Code { get; set; }
    public string? Message { get; set; }
}