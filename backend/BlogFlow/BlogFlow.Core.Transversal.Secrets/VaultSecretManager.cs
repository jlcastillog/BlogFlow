using Microsoft.Extensions.Options;
using System.Text.Json;
using VaultSharp;
using VaultSharp.V1.AuthMethods.Token;
using VaultSharp.V1.Commons;

namespace BlogFlow.Core.Transversal.Secrets
{
    public interface ISecretManager
    {
        Task<T> Get<T>(string path) where T : new();
    }

    internal class VaultSecretManager : ISecretManager
    {
        private readonly VaultSettings _vaultSettings;

        public VaultSecretManager(IOptions<VaultSettings> vaultSettings)
        {
            _vaultSettings = vaultSettings.Value with { TokenApi = GetTokenFromEnvironmentVariable(), 
                                                        VaultUrl = GetUrlFromEnvironmentVariable() };
        }

        public async Task<T> Get<T>(string path)
            where T : new()
        {
            VaultClient client = new VaultClient(new VaultClientSettings(_vaultSettings.VaultUrl,
                new TokenAuthMethodInfo(_vaultSettings.TokenApi)));

            Secret<SecretData> kv2Secret = await client.V1.Secrets.KeyValue.V2
                .ReadSecretAsync(path: path, mountPoint: "secret");
            var returnedData = kv2Secret.Data.Data;

            var json = JsonSerializer.Serialize(returnedData);

            return JsonSerializer.Deserialize<T>(json);
        }

        private string GetTokenFromEnvironmentVariable()
            => Environment.GetEnvironmentVariable("VAULT_TOKEN")
               ?? throw new NotImplementedException("please specify the VAULT-TOKEN env_var");

        private string GetUrlFromEnvironmentVariable()
                => Environment.GetEnvironmentVariable("VAULT_ADDR")
                   ?? throw new NotImplementedException("please specify the VAULT_ADDR env_var");
    }
}
