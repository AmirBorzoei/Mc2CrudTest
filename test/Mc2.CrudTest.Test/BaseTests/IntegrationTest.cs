using System;
using System.Transactions;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Mc2.CrudTest.Test.BaseTests;

public abstract class IntegrationTest
{
    private readonly TransactionScopeAsyncFlowOption _asyncFlowOption;
    private readonly IsolationLevel _isolationLevel;
    private readonly TransactionScopeOption _scopeOption;
    protected readonly WebApplicationFactory<Program> Application;
    protected readonly HttpClient Client;
    private TransactionScope? _scope;

    protected IntegrationTest()
    {
        _asyncFlowOption = TransactionScopeAsyncFlowOption.Enabled;
        _isolationLevel = IsolationLevel.ReadCommitted;
        _scopeOption = TransactionScopeOption.Required;
        
        Application = new WebApplicationFactory<Program>()
            .WithWebHostBuilder(builder =>
            {
                builder.ConfigureAppConfiguration((c, b) => { b.AddJsonFile("appsettings.Test.json"); });
                builder.UseTestServer(testServerOptions => testServerOptions.PreserveExecutionContext = true);

                builder.ConfigureServices(services =>
                {
                    services.AddTransient<Migrator.Migrator>();
                });
            });

        IServiceScope scope = Application.Services.CreateScope();
        Migrator.Migrator migrator = scope.ServiceProvider.GetService<Migrator.Migrator>()!;
        migrator.Migrate();

        Client = Application.CreateClient();
    }

    [Before]
    protected virtual void Before()
    {
        TransactionOptions options = new() { IsolationLevel = _isolationLevel };
        _scope = new TransactionScope(_scopeOption, options, _asyncFlowOption);
    }

    [After]
    protected virtual void After()
    {
        _scope?.Dispose();
    }
}