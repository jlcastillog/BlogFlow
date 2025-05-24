# Define the directory where the docker-compose.yml file is located
$composeFilePath = "."

# Navigate to the project directory
Set-Location $composeFilePath

$composeFile = Join-Path $composeFilePath "docker-compose.yml"
if (-Not (Test-Path $composeFile)) {
    Write-Host $composeFile
    Write-Host "Not found docker-compose.yml in $composeFilePath" -ForegroundColor Red
    exit 1
}

# Show running containers based on the compose file
Write-Host "Stopping and removing existing containers..." -ForegroundColor Yellow
docker compose down -v

# (Optional) Remove old images to force a rebuild
Write-Host "Removing old images..." -ForegroundColor Yellow
$images = docker images --format "{{.Repository}}:{{.Tag}}" | Where-Object { $_ -match "yourimageprefix" }
foreach ($image in $images) {
    Write-Host "Removing image: $image"
    docker rmi $image -f
}

# Rebuild and redeploy the applications
Write-Host "Rebuilding and deploying applications..." -ForegroundColor Green
docker compose up --build -d

Write-Host "Waiting for Vault to be ready..." -ForegroundColor Yellow
$maxRetries = 10
$retryCount = 0
$vaultReady = $false

while (-not $vaultReady -and $retryCount -lt $maxRetries) {
    try {
        $status = docker exec vault vault status
        if ($status -match "sealed.*false") {
            $vaultReady = $true
        } else {
            throw "Vault is sealed"
        }
    } catch {
        Write-Host "Vault not ready yet. Retrying in 5 seconds..."
        Start-Sleep -Seconds 5
        $retryCount++
    }
}

if (-not $vaultReady) {
    Write-Host "Vault did not become ready. Exiting." -ForegroundColor Red
    exit 1
}

# Load vault-init-secrets.ps1 to access the function
. "$PSScriptRoot\vault\vault-init-secrets.ps1"

# LoadVault secrets
Write-Host "Loading Vault secrets..." -ForegroundColor Green
VaultInitSecrets

Start-Sleep -Seconds 5

Write-Host "Running APIs..." -ForegroundColor Green
docker compose up -d web-service-auth
docker compose up -d web-service-core

# Check the final status of the containers
Write-Host "Final container status:" -ForegroundColor Cyan
docker ps