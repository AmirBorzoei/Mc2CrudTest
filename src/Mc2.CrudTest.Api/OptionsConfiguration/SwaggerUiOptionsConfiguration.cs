using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace Mc2.CrudTest.Api.OptionsConfiguration;

public class SwaggerUiOptionsConfiguration : IConfigureOptions<SwaggerUIOptions>
{
    private readonly IApiVersionDescriptionProvider _apiVersionDescriptionProvider;

    public SwaggerUiOptionsConfiguration(IApiVersionDescriptionProvider apiVersionDescriptionProvider)
    {
        _apiVersionDescriptionProvider = apiVersionDescriptionProvider;
    }

    public void Configure(SwaggerUIOptions options)
    {
        _apiVersionDescriptionProvider.ApiVersionDescriptions.ToList().ForEach(c =>
            options.SwaggerEndpoint($"/swagger/{c.GroupName}/swagger.json", c.GroupName.ToUpperInvariant()));
    }
}