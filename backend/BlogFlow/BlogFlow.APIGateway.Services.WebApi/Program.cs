using BlogFlow.APIGateway.Services.WebApi.Helpers;
using BlogFlow.APIGateway.Services.WebApi.Modules.Features;
using BlogFlow.APIGateway.Services.WebApi.Modules.HealthCheck;
using BlogFlow.APIGateway.Services.WebApi.Modules.Logger;
using BlogFlow.APIGateway.Services.WebApi.Modules.Authentication;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

builder.AddLogger();

// Detect current environment
var environment = builder.Environment.EnvironmentName;

Console.WriteLine($"Running in: {environment}");

builder.Services.Configure<HealthCheckSettings>(
    builder.Configuration.GetSection("HealthChecks"));

builder.Services.AddAuthentication(builder.Configuration);
builder.Services.AddFeature(builder.Configuration);
builder.Services.AddHealthCheck(builder.Configuration);

var app = builder.Build();

app.UseCors(FeatureExtension.myPolicy);
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapReverseProxy(proxyPipeline =>
{
    proxyPipeline.UseAuthentication();
    proxyPipeline.UseAuthorization();
});

app.MapHealthChecks("/health", new HealthCheckOptions
{
    ResponseWriter = async (context, report) =>
    {
        context.Response.ContentType = "application/json";
        var result = JsonSerializer.Serialize(new
        {
            status = report.Status.ToString(),
            checks = report.Entries.Select(entry => new
            {
                name = entry.Key,
                status = entry.Value.Status.ToString(),
                exception = entry.Value.Exception?.Message,
                duration = entry.Value.Duration.ToString()
            })
        });
        await context.Response.WriteAsync(result);
    }
});

app.MapHealthChecksUI(options =>
{
    options.UIPath = "/health-ui";
});

app.Run();
