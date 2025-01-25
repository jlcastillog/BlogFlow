# BACKEND

## Modulo de autenticación - BlogFlow.Auth

Para generar la migración inicial hacen falta ejecutar estos comandos.

1. Crear la migración inical:

```shell
dotnet ef migrations add InitialCreate --project .\BlogFlow.Auth.Persistence\ --startup-project .\BlogFlow.Auth.Services.WebApi\
```
2. Pasarlo a la base de datos:

```shell
dotnet ef database update --project .\BlogFlow.Auth.Persistence\ --startup-project .\BlogFlow.Auth.Services.WebApi\
```

## Dockerización

Para un uso mas comodo de la API sin tener que arrancar el visual studio he tomado la decisión de dockerizarla. Es necesario tener descargado docker en local.
Para la dockerización es necesario crear un archivo Dokerfile con la configuración de la imagen.

Para generar la imagen es necesario de ejecutar este comando desde el directorio del archivo Dokerfile:

```
docker build -t blogflow-auth-webapi:latest .
```

Una vez generado la imagen para arranacar el contenedor es necesario ejecutar este comando:

```
docker run -d -p 5000:8080 --name BlogFlow.Auth.WebApi blogflow-auth-webapi:latest
```

Se puede probar la aip poneindo en el navegador esta url:

http://localhost:5000/api/v1/Users/GetAll