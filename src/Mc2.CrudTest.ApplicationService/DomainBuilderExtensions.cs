using Mc2.CrudTest.Domain.Customer;
using Microsoft.Extensions.DependencyInjection;

namespace Mc2.CrudTest.ApplicationService;

public static class DomainBuilderExtensions
{
    public static IServiceCollection AddDomainBuilders(this IServiceCollection services)
    {
        services.AddTransient<CustomerBuilder, CustomerBuilder>();

        return services;
    }
}