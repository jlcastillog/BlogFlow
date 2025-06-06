using Asp.Versioning.ApiExplorer;
using BlogFlow.Auth.Services.WebApi.Modules.Feature;
using BlogFlow.Auth.Services.WebApi.Modules.HealthChecks;
using BlogFlow.Auth.Services.WebApi.Modules.Swagger;
using BlogFlow.Auth.Services.WebApi.Modules.Versioning;
using BlogFlow.Core.Application.UseCases;
using BlogFlow.Core.Infrastructure.Persistence;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using BlogFlow.Auth.Services.WebApi.Modules.Logger;

var builder = WebApplication.CreateBuilder(args);

// Detect current environment
var environment = builder.Environment.EnvironmentName;

Console.WriteLine($"Running in: {environment}");

// Set appsettings by environment
builder.Configuration
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{environment}.json", optional: true)
    .AddEnvironmentVariables();

builder.AddLogger();

builder.Services.AddEndpointsApiExplorer();

// Add services to the container.
builder.Services.AddFeature(builder.Configuration);
builder.Services.AddPersistenceServices(builder.Configuration);
builder.Services.AddApplicationServices();
builder.Services.AddVersioning();
builder.Services.AddSwagger();
builder.Services.AddHealthCheck(builder.Configuration);

builder.Services.AddHttpsRedirection(options =>
{
    options.HttpsPort = null; // Temporarily do not redirect to HTTPS to run from Docker
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    var provider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        //Build a swagger endpoint for each discovered API version
        foreach (var description in provider.ApiVersionDescriptions)
        {
            c.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", description.GroupName.ToUpperInvariant());
        }
    });
}
app.UseCors(FeatureExtension.myPolicy);
app.UseHttpsRedirection();
app.MapControllers();
app.MapHealthChecks("/health", new HealthCheckOptions
{
    Predicate = _ => true,
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});

app.Run();
