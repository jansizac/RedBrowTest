# RedBrowTest API

## Descripción
RedBrowTest es una API desarrollada en .NET Core 7, diseñada para ejecutarse en un entorno de contenedores utilizando Docker Compose. Esta API se integra con una base de datos SQL Server, proporcionando un entorno listo para el desarrollo y la prueba de funcionalidades de autenticación y gestión de usuarios.

## Requisitos Previos
Para ejecutar este proyecto, necesitarás tener instalados los siguientes componentes en tu sistema:
- Visual Studio 2022
- Docker Desktop
- .NET Core 7 SDK

## Configuración
Se proporcionará un archivo `.env` con todas las variables de entorno necesarias configuradas. Asegúrate de que este archivo esté en el directorio raíz del proyecto antes de iniciar la aplicación.

## Ejecución de la Aplicación
Para iniciar la aplicación y la base de datos SQL Server, ejecuta los siguientes comandos en la terminal desde el directorio raíz del proyecto:

```bash
docker-compose build
docker-compose up
```

Esto iniciará todos los servicios necesarios definidos en el archivo docker-compose.yml.

O si prefieres ejecutarlo desde VS2022, abre la solución, selecciona como proyecto de inicio el proyecto docker-compose y ejecutalo.

## Acceso a la API
Una vez que la aplicación esté en ejecución, podrás acceder a ella usando la url que se abrira al ejecutarla

### Autenticación
Puedes iniciar sesión utilizando el siguiente usuario predeterminado:

- Usuario: admin@mail.com
- Contraseña: 123456

Utiliza estas credenciales para autenticarte  usando el controlador Authentication y accede a las funcionalidades del controlador Usuarios.

## Contácto
Si tienes preguntas o deseas contribuir al proyecto, no dudes en contactarme en jansizac@hotmail.com.
