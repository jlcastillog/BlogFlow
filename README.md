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

# Almacenamiento de ficheros

Para el alamcenamiento de ficheros (por ahora imagenes) he optado por usar un almacenamiento en la nube usando [Cloudinary](https://cloudinary.com/).
He optado principalmente por esta opcion por:

1. Escalabilidad
   - Puedes almacenar desde unos pocos archivos hasta petabytes sin preocuparte por la infraestructura.
   - Se adapta automáticamente al crecimiento de tu aplicación y a la demanda de usuarios.

2. Alta disponibilidad y redundancia
   - Los proveedores en la nube como AWS S3, Azure Blob Storage o Google Cloud Storage replican los datos en múltiples ubicaciones.
   - Esto garantiza que los archivos estén disponibles incluso ante fallos de hardware o centros de datos.

3. Reducción de costos operativos
   - No necesitas invertir en servidores propios ni preocuparte por el mantenimiento físico.
   - Puedes pagar solo por lo que usas (almacenamiento, transferencias, solicitudes).

4. Integración fácil con CDNs
   - Puedes conectar el almacenamiento a una Red de Distribución de Contenidos (CDN) para servir archivos estáticos (imágenes, vídeos, PDFs, etc.) rápidamente a usuarios de todo el mundo.

5. Seguridad y control de acceso
   - Soporte para encriptación en reposo y en tránsito.
   - Gestión detallada de permisos mediante roles y políticas (IAM, ACLs, etc.).

6. Backups y versiones
   - Posibilidad de mantener versiones de archivos y restaurarlos en caso de errores o eliminación accidental.

7. Facilidad de integración
   - APIs y SDKs disponibles para múltiples lenguajes y frameworks.
   - Ideal para aplicaciones que requieren subir y servir archivos desde el frontend (por ejemplo, React + backend en .NET).

8. Optimización del rendimiento
   - Al externalizar la entrega de archivos, reduces la carga del servidor principal de tu aplicación.
