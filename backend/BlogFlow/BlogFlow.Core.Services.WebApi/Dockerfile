# Imagen base oficial de .NET para compilar y ejecutar la aplicación
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
EXPOSE 80

# Copiar archivos de la solución completa
COPY ["BlogFlow.sln", "./"]
COPY ["BlogFlow.APIGateway.Services.WebApi/BlogFlow.APIGateway.Services.WebApi.csproj", "BlogFlow.APIGateway.Services.WebApi/"]
COPY ["BlogFlow.Auth.Services.WebApi/BlogFlow.Auth.Services.WebApi.csproj", "BlogFlow.Auth.Services.WebApi/"]
COPY ["BlogFlow.Core.Services.WebApi/BlogFlow.Core.Services.WebApi.csproj", "BlogFlow.Core.Services.WebApi/"]
COPY ["BlogFlow.Notifications.Worker/BlogFlow.Notifications.Worker.csproj", "BlogFlow.Notifications.Worker/"]
COPY ["BlogFlow.Core.Infrastructure.Persistence/BlogFlow.Core.Infrastructure.Persistence.csproj", "BlogFlow.Core.Infrastructure.Persistence/"]
COPY ["BlogFlow.Core.Infrastructure.Mail/BlogFlow.Core.Infrastructure.Mail.csproj", "BlogFlow.Core.Infrastructure.Mail/"]
COPY ["BlogFlow.Core.Domain/BlogFlow.Core.Domain.csproj", "BlogFlow.Core.Domain/"]
COPY ["BlogFlow.Core.Application.UseCases/BlogFlow.Core.Application.UseCases.csproj", "BlogFlow.Core.Application.UseCases/"]
COPY ["BlogFlow.Core.Application.Interface/BlogFlow.Core.Application.Interface.csproj", "BlogFlow.Core.Application.Interface/"]
COPY ["BlogFlow.Core.Application.DTO/BlogFlow.Core.Application.DTO.csproj", "BlogFlow.Core.Application.DTO/"]
COPY ["BlogFlow.Core.Transversal.Common/BlogFlow.Core.Transversal.Common.csproj", "BlogFlow.Core.Transversal.Common/"]
COPY ["BlogFlow.Auth.Application.Test/BlogFlow.Auth.Application.Test.csproj", "BlogFlow.Auth.Application.Test/"]

# Restaurar dependencias
RUN dotnet restore "BlogFlow.sln"

# Copiar el resto del código fuente
COPY . .

# Compilar la solución
WORKDIR /src/BlogFlow.Core.Services.WebApi
RUN dotnet publish -c Release -o /app/publish

# Crear la imagen final basada en el runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app

# Instalar PowerShell Core
RUN apt-get update \
    && apt-get install -y wget apt-transport-https software-properties-common \
    && wget -q "https://packages.microsoft.com/config/debian/11/packages-microsoft-prod.deb" -O packages-microsoft-prod.deb \
    && dpkg -i packages-microsoft-prod.deb \
    && apt-get update \
    && apt-get install -y powershell \
    && rm -rf /var/lib/apt/lists/*

# Copiar el script de espera
COPY BlogFlow.Core.Services.WebApi/wait-for-sql.ps1 .

COPY --from=build /app/publish .
ENTRYPOINT ["pwsh", "./wait-for-sql.ps1", "dotnet", "BlogFlow.Core.Services.WebApi.dll"]