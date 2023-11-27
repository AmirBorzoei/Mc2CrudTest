namespace Mc2.CrudTest.Api.ExceptionHandling.Handler;

public interface IExceptionHandler
{
    void Handle(object context, Exception ex);
}