using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Mc2.CrudTest.Api.OptionsConfiguration;

public class SwaggerGenOptionsConfiguration : IConfigureOptions<SwaggerGenOptions>
{
    private readonly IApiVersionDescriptionProvider _provider;

    public SwaggerGenOptionsConfiguration(IApiVersionDescriptionProvider provider)
    {
        _provider = provider;
    }

    public void Configure(SwaggerGenOptions options)
    {
        foreach (ApiVersionDescription desc in _provider.ApiVersionDescriptions)
        {
            options.SwaggerDoc(desc.GroupName, CreateInfoForApiVersion(desc));
        }
    }

    private static OpenApiInfo CreateInfoForApiVersion(ApiVersionDescription desc)
    {
        OpenApiInfo info = new()
        {
            Title = "Mc2 Test API",
            Version = desc.ApiVersion.ToString(),
            Description = "Mc2 Test Rest API"
        };

        if (desc.IsDeprecated)
        {
            info.Description += " This API version has been deprecated.";
        }

        return info;
    }
}