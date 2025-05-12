using BlogFlow.APIGateway.Services.WebApi.Modules.Features;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddFeature(builder.Configuration);

var app = builder.Build();

app.UseCors(FeatureExtension.myPolicy);
app.UseHttpsRedirection();
app.MapReverseProxy();

app.Run();
