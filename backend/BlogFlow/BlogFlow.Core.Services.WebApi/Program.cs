using Asp.Versioning.ApiExplorer;
using BlogFlow.Core.Services.WebApi.Modules.Authentication;
using BlogFlow.Core.Services.WebApi.Modules.Feature;
using BlogFlow.Core.Services.WebApi.Modules.Swagger;
using BlogFlow.Core.Services.WebApi.Modules.Versioning;
using BlogFlow.Core.Infrastructure.Persistence;
using BlogFlow.Core.Application.UseCases;
using System;
using BlogFlow.Core.Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

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

builder.Services.AddEndpointsApiExplorer();

// Add services to the container.
builder.Services.AddFeature(builder.Configuration);
builder.Services.AddPersistenceServices(builder.Configuration);
builder.Services.AddApplicationServices();
builder.Services.AddAuthentication(builder.Configuration);
builder.Services.AddVersioning();
builder.Services.AddSwagger();

builder.Services.AddHttpsRedirection(options =>
{
    options.HttpsPort = null; // Temporarily do not redirect to HTTPS to run from Docker
});

var app = builder.Build();

// Ejecuta migraciones al iniciar
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    db.Database.Migrate(); // Apply pending migrations
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
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
