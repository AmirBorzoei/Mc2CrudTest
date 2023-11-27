using Mc2.CrudTest.Api.ExceptionHandling.Policies;

namespace Mc2.CrudTest.Api.ExceptionHandling.Handler;

public class ExceptionHandler : IExceptionHandler
{
    private readonly ILogger<ExceptionHandler> _logger;
    private readonly IEnumerable<IExceptionPolicy> _policies;

    public ExceptionHandler(IEnumerable<IExceptionPolicy> exceptionPolicies, ILogger<ExceptionHandler> logger)
    {
        _policies = exceptionPolicies;
        _logger = logger;
    }

    public void Handle(object context, Exception ex)
    {
        _logger.LogError(ex, ex.Message);

        _policies.OrderBy(p => p.Order)
            .First(p => p.IsEligible(ex))
            .Apply(context, ex);
    }
}