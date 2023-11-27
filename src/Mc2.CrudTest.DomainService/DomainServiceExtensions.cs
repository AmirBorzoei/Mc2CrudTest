using Mc2.CrudTest.Domain.Customer.DomainService;
using Mc2.CrudTest.DomainService.Customer;
using Microsoft.Extensions.DependencyInjection;

namespace Mc2.CrudTest.DomainService;

public static class DomainServiceExtensions
{
    public static IServiceCollection AddDomainServices(this IServiceCollection services)
    {
        services.AddTransient<ICustomerUniqueCheckService, CustomerUniqueCheckService>();
        services.AddTransient<IEMailUniqueCheckService, EMailUniqueCheckService>();

        return services;
    }
}