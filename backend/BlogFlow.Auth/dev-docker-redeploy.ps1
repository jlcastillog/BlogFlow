# Configuración
$imageName = "blogflow-auth-webapi"             # Nombre de la imagen local
$containerName = "BlogFlow.Auth.WebApi"         # Nombre del contenedor
$dockerfilePath = ".\Dockerfile"                # Ruta al Dockerfile (cámbiala si es necesario)

# Verificar si el contenedor está en ejecución
Write-Host "Verificando si el contenedor '$containerName' está en ejecución..."
$container = docker ps -q --filter "name=$containerName"

if ($container) {
    Write-Host "Deteniendo el contenedor '$containerName'..."
    docker stop $containerName
    Write-Host "Eliminando el contenedor '$containerName'..."
    docker rm $containerName
} else {
    Write-Host "No se encontró un contenedor en ejecución con el nombre '$containerName'."
}

# Construir la nueva imagen
Write-Host "Construyendo la nueva imagen Docker '$imageName' desde el Dockerfile en '$dockerfilePath'..."
docker build -t $imageName -f $dockerfilePath .
if ($LASTEXITCODE -ne 0) {
    Write-Host "Error al construir la imagen Docker. Verifica el Dockerfile." -ForegroundColor Red
    exit 1
}

# Iniciar un nuevo contenedor
Write-Host "Iniciando un nuevo contenedor con la imagen '$imageName'..."
docker run -d --name $containerName -p 5000:8080 $imageName

Write-Host "El contenedor '$containerName' ha sido desplegado con éxito."

# Limpiar imágenes no utilizadas
Write-Host "Limpiando imágenes no utilizadas..."
docker image prune -f 
