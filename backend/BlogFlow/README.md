# BACKEND

## Modulo de autenticación - BlogFlow.Auth

Para generar la migración inicial hacen falta ejecutar estos comandos.

1. Crear la migración inical:

```shell
dotnet ef migrations add InitialCreate --project .\BlogFlow.Core.Infrastructure.Persistence\ --startup-project .\BlogFlow.Auth.Services.WebApi\
```
2. Pasarlo a la base de datos:

```shell
dotnet ef database update --project .\BlogFlow.Core.Infrastructure.Persistence\ --startup-project .\BlogFlow.Auth.Services.WebApi\
```

## Sistema de Log

Se ha optado por usar el paquete Serilog debido a sus caracteristicas:

- Soporte de logging estructurado: ideal para análisis con herramientas como Seq, Elastic, etc.
- Gran cantidad de sinks (destinos) disponibles: archivos, consola, bases de datos, sistemas distribuidos.
- Fácil configuración y enriquecimiento de logs con propiedades personalizadas.
- Compatible con ILogger.

<div style="display: flex; align-items: center;">
<p>Para la monitorización de los log usaremos <a src="https://datalust.co/seq">Seq</a></p>
<img src="https://img.stackshare.io/service/4284/seq-nuget.png" width="40" height="40">
</div>

Dentro de la web de Seq se pueden crear filtros para los logs de cada servicio creando señales para guardar filtros
Puedes crear señales (se guardan en la barra lateral) para cada servicio. Por ejemplo:

- Señal: Logs - APIGateway → Filtro: Service = 'APIGateway'
- Señal: Logs - APICore → Filtro: Service = 'APICore'
- Señal: Logs - APIAuth → Filtro: Service = 'APIAuth'