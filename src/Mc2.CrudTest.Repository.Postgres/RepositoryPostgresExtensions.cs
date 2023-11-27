using Mc2.CrudTest.Domain.Customer.DomainService;
using Mc2.CrudTest.Repository.Postgres.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Mc2.CrudTest.Repository.Postgres;

public static class RepositoryPostgresExtensions
{
    public static IServiceCollection AddPostgresRepositories(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<Mc2CrudTestDbContext>(option => option.UseNpgsql(configuration.GetConnectionString("Mc2CrudTestDbConnection")));

        services.AddTransient<ICustomerRepository, CustomerRepository>();

        return services;
    }
}