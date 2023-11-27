using System.Reflection;
using Mc2.CrudTest.Api.ExceptionHandling.Handler;
using Mc2.CrudTest.Api.ExceptionHandling.Policies;

namespace Mc2.CrudTest.Api.ExceptionHandling;

public static class ExceptionHandlingExtensions
{
    public static IServiceCollection AddExceptionHandling(this IServiceCollection services)
    {
        services.AddTransient<IExceptionHandler, ExceptionHandler>();

        Assembly.GetExecutingAssembly().GetTypes()
            .Where(t => t.IsClass && !t.IsAbstract && t.IsAssignableTo(typeof(HttpFaultProvider)))
            .ToList()
            .ForEach(t => t.GetInterfaces().ToList().ForEach(i => services.AddTransient(i, t)));

        return services;
    }
}