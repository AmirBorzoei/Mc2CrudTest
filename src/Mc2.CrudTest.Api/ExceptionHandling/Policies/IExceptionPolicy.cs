namespace Mc2.CrudTest.Api.ExceptionHandling.Policies;

public interface IExceptionPolicy
{
    int Order { get; }

    bool IsEligible(Exception ex);

    void Apply(object context, Exception ex);
}