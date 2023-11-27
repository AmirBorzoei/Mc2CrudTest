using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.Extensions.Options;

namespace Mc2.CrudTest.Api.OptionsConfiguration;

public class CorsOptionsConfiguration : IConfigureOptions<CorsOptions>
{
    public const string AllowAllCors = "AllowAllCors";

    public void Configure(CorsOptions options)
    {
        options.AddPolicy(AllowAllCors, corsPolicyBuilder =>
        {
            corsPolicyBuilder.SetIsOriginAllowed(_ => true)
                .AllowCredentials()
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
    }
}