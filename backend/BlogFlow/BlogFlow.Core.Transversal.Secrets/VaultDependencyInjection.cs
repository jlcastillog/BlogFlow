using BlogFlow.Core.Transversal.Secrets;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

public static class VaultDependencyInjection
{
    /// <summary>
    ///  add vault service to the service collection
    /// </summary>
    public static void AddVaultService(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        serviceCollection.Configure<VaultSettings>(configuration.GetSection("SecretManager"));
        serviceCollection.AddScoped<ISecretManager, VaultSecretManager>();
    }
}
