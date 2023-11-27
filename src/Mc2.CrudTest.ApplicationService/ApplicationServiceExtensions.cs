using Mc2.CrudTest.ApplicationService.Contract.Customer;
using Microsoft.Extensions.DependencyInjection;

namespace Mc2.CrudTest.ApplicationService;

public static class ApplicationServiceExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddTransient<ICustomerApplicationService, CustomerApplicationService>();

        return services;
    }
}