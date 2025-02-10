# BLOGFLOW

Aplicación para la gestion de blogs, pasando por la visualización, creación y edicion para usuarios.

## Dockerización (dev en local)

He automatizado la creación de contenderores docker para desplegar en local toda la aplicación. Desde el frontend hasta las APIs del backend.

Se pueden crear cada una de las imagenes y ejecutar un contenedor de ellas individualmente de la siguiente manera:

- Para generar la imagen es necesario de ejecutar este comando desde el directorio del archivo Dokerfile:

```
docker build -t blogflow-auth-webapi:latest .
```

- Una vez generado la imagen para arranacar el contenedor es necesario ejecutar este comando:

```
docker run -d -p 5000:8080 --name BlogFlow.Auth.WebApi blogflow-auth-webapi:latest
```

Usando docker compose (archivo docker-compose.yml) se realiza la creación de todas las images de la aplicación:

- web-service-core
- web-service-auth
- frontend

He creado un script de powershell en la raiz de la solución que automatiza el redespliegue en docker de todas la aplicación (dev-docker-redeploy.ps1).