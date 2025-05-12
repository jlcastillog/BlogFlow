using BlogFlow.APIGateway.Services.WebApi.Modules.Features;

var builder = WebApplication.CreateBuilder(args);

// Detect current environment
var environment = builder.Environment.EnvironmentName;

Console.WriteLine($"Running in: {environment}");

builder.Services.AddFeature(builder.Configuration);

var app = builder.Build();

app.UseCors(FeatureExtension.myPolicy);
app.UseHttpsRedirection();
app.MapReverseProxy();

app.Run();
