namespace Mc2.CrudTest.Api.OptionsConfiguration;

public static class OptionsConfigurationsExtensions
{
    public static IServiceCollection AddOptionsConfigurations(this IServiceCollection services)
    {
        services.ConfigureOptions<CorsOptionsConfiguration>();

        services.ConfigureOptions<ApiVersioningOptionsConfiguration>();
        services.ConfigureOptions<ApiExplorerOptionsConfiguration>();

        services.ConfigureOptions<SwaggerGenOptionsConfiguration>();
        services.ConfigureOptions<SwaggerUiOptionsConfiguration>();

        return services;
    }
}