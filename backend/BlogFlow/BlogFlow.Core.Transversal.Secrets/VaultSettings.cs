namespace BlogFlow.Core.Transversal.Secrets
{
    record class VaultSettings
    {
        public string? VaultUrl { get; set; }
        public string? TokenApi { get; set; }
    }
}
