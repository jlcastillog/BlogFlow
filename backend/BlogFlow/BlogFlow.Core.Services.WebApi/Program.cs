using Asp.Versioning.ApiExplorer;
using BlogFlow.Core.Application.UseCases;
using BlogFlow.Core.Infrastructure.Persistence;
using BlogFlow.Core.Infrastructure.Persistence.Contexts;
using BlogFlow.Core.Services.WebApi.Modules.Feature;
using BlogFlow.Core.Services.WebApi.Modules.HealthChecks;
using BlogFlow.Core.Services.WebApi.Modules.Logger;
using BlogFlow.Core.Services.WebApi.Modules.Swagger;
using BlogFlow.Core.Services.WebApi.Modules.Versioning;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;

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

// Running migration on starting
using (var scope = app.Services.CreateScope())
{
    Console.WriteLine("Checking for database and applying migrations if needed");
    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

    var databaseCreator = (RelationalDatabaseCreator)dbContext.Database.GetService<IDatabaseCreator>();
    if (!databaseCreator.Exists())
    {
        Console.WriteLine("Database does not exist. Creating...");
        dbContext.Database.EnsureCreated(); 
    }

    dbContext.Database.Migrate();
}

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
