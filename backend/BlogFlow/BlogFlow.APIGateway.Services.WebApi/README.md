# BlogFlow.APIGateway.Services.WebApi

Una API Gateway es una capa extra que añadimos entre ya bien sean nuestro servicios Front End, o clientes externos para que se comuniquen con ella en vez de con el servicio Back end correspondiente.

Una API Gateway actúa como un reverse proxy al que podemos incluir funcionalidades extra, como pueden ser seguridad, políticas de uso, alertas, etc.

Lo primero que vamos a hacer es identificar los endpoints que tenemos actualmente:

- Authentication (login and users): https://localhost:7257, http://localhost:5207
- Core (management of blogs, posts, ...): https://localhost:7198, http://localhost:5117