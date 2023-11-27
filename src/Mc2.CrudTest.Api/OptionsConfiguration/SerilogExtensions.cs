using Serilog;
using Serilog.Core;
using Serilog.Exceptions;

namespace Mc2.CrudTest.Api.OptionsConfiguration;

public static class SerilogExtensions
{
    public static void ConfigureSerilogLogging(this WebApplicationBuilder builder)
    {
        const string outputTemplate = "[{Timestamp:HH:mm:ss} {Level:u3}] [{SourceContext}] {Message:lj}{NewLine}{Exception}";

        Logger logger = new LoggerConfiguration()
            .WriteTo.Debug(outputTemplate: outputTemplate)
            .WriteTo.Console(outputTemplate: outputTemplate)
            .Enrich.WithProperty("Environment", builder.Environment.EnvironmentName)
            .Enrich.FromLogContext()
            .Enrich.WithExceptionDetails()
            .ReadFrom.Configuration(builder.Configuration)
            .CreateLogger();

        builder.Logging.ClearProviders();
        builder.Logging.AddSerilog(logger);
    }
}