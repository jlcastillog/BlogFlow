# Define the directory where the docker-compose.yml file is located
$composeFilePath = "."

# Navigate to the project directory
Set-Location $composeFilePath

# Show running containers based on the compose file
Write-Host "Stopping and removing existing containers..." -ForegroundColor Yellow
#docker compose down -v
docker compose down

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

# Check the final status of the containers
Write-Host "Final container status:" -ForegroundColor Cyan
docker ps