using Asp.Versioning.ApiExplorer;
using BlogFlow.Auth.Application.UseCases;
using BlogFlow.Auth.Persistence;
using BlogFlow.Auth.Services.WebApi.Modules.Authentication;
using BlogFlow.Auth.Services.WebApi.Modules.Feature;
using BlogFlow.Auth.Services.WebApi.Modules.Swagger;
using BlogFlow.Auth.Services.WebApi.Modules.Versioning;

var builder = WebApplication.CreateBuilder(args);

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
