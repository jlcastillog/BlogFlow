# Imagen base oficial de .NET para compilar y ejecutar la aplicación
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
EXPOSE 80

# Copiar archivos de la solución completa
COPY ["BlogFlow.sln", "./"]
COPY ["BlogFlow.APIGateway.Services.WebApi/BlogFlow.APIGateway.Services.WebApi.csproj", "BlogFlow.APIGateway.Services.WebApi/"]
COPY ["BlogFlow.Auth.Services.WebApi/BlogFlow.Auth.Services.WebApi.csproj", "BlogFlow.Auth.Services.WebApi/"]
COPY ["BlogFlow.Core.Services.WebApi/BlogFlow.Core.Services.WebApi.csproj", "BlogFlow.Core.Services.WebApi/"]
COPY ["BlogFlow.Core.Infrastructure.Persistence/BlogFlow.Core.Infrastructure.Persistence.csproj", "BlogFlow.Core.Infrastructure.Persistence/"]
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
WORKDIR /src/BlogFlow.APIGateway.Services.WebApi
RUN dotnet publish -c Release -o /app/publish

# Crear la imagen final basada en el runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "BlogFlow.APIGateway.Services.WebApi.dll"]