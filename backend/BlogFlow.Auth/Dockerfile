# Imagen base oficial de .NET para compilar y ejecutar la aplicación
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
EXPOSE 80

# Copiar archivos de la solución completa
COPY ["BlogFlow.Auth.sln", "./"]
COPY ["BlogFlow.Auth.Services.WebApi/BlogFlow.Auth.Services.WebApi.csproj", "BlogFlow.Auth.Services.WebApi/"]
COPY ["BlogFlow.Auth.Persistence/BlogFlow.Auth.Persistence.csproj", "BlogFlow.Auth.Persistence/"]
COPY ["BlogFlow.Auth.Domain/BlogFlow.Auth.Domain.csproj", "BlogFlow.Auth.Domain/"]
COPY ["BlogFlow.Auth.Application.UseCases/BlogFlow.Auth.Application.UseCases.csproj", "BlogFlow.Auth.Application.UseCases/"]
COPY ["BlogFlow.Auth.Application.Interface/BlogFlow.Auth.Application.Interface.csproj", "BlogFlow.Auth.Application.Interface/"]
COPY ["BlogFlow.Auth.Application.DTO/BlogFlow.Auth.Application.DTO.csproj", "BlogFlow.Auth.Application.DTO/"]
COPY ["BlogFlow.Auth.Transversal.Common/BlogFlow.Auth.Transversal.Common.csproj", "BlogFlow.Auth.Transversal.Common/"]
COPY ["BlogFlow.Auth.Application.Test/BlogFlow.Auth.Application.Test.csproj", "BlogFlow.Auth.Application.Test/"]

# Restaurar dependencias
RUN dotnet restore "BlogFlow.Auth.sln"

# Copiar el resto del código fuente
COPY . .

# Compilar la solución
WORKDIR /src/BlogFlow.Auth.Services.WebApi
RUN dotnet publish -c Release -o /app/publish

# Crear la imagen final basada en el runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "BlogFlow.Auth.Services.WebApi.dll"]