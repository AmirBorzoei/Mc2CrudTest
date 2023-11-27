using System.Text.Json.Serialization;
using Mc2.CrudTest.Api.ExceptionHandling;
using Mc2.CrudTest.Api.ExceptionHandling.Middleware;
using Mc2.CrudTest.Api.OptionsConfiguration;
using Mc2.CrudTest.ApplicationService;
using Mc2.CrudTest.DomainService;
using Mc2.CrudTest.Repository.Postgres;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplicationServices();
builder.Services.AddDomainBuilders();
builder.Services.AddDomainServices();
builder.Services.AddPostgresRepositories(builder.Configuration);
builder.Services.AddExceptionHandling();
builder.Services.AddOptionsConfigurations();
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        policy =>
        {
            policy.AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});
builder.Services.AddApiVersioning();
builder.Services.AddVersionedApiExplorer();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.ConfigureSerilogLogging();

builder.Services.AddControllers()
    .AddJsonOptions(options => { options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()); });


WebApplication app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionHandlerMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseCors();

app.Run();

public partial class Program { }