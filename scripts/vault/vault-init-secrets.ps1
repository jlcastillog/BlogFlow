function VaultInitSecrets {
    Write-Host "VaultInitSecrets..." -ForegroundColor Green
    # import file with relevant data
    $config = Import-PowerShellDataFile -Path "./scripts/vault/secrets.psd1"

    # Vault token
    $vaultToken = $config.VaultToken

    # Database Connection string
    $connectionString = $config.ConnectionString

    # Create secrets in Vault
    Write-Host "Creating secrets in Vault" -ForegroundColor Green
    docker exec -e VAULT_TOKEN=$vaultToken vault vault kv put -mount=secret BlogFlowConnection ConnectionString=$connectionString
}

