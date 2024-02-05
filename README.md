## Ejecución del Proyecto

El proyecto puede ser ejecutado de dos maneras:

### Desde Visual Studio 2022

1. Abre el proyecto en Visual Studio 2022.
2. Selecciona el perfil de Docker-Compose.
3. Ejecuta el proyecto desde Visual Studio.

### Desde la Raíz del Proyecto

1. Abre una terminal en la raíz del proyecto.
2. Ejecuta el siguiente comando:
   
   ```bash
   docker-compose -f docker-compose.yml -f docker-compose.override.yml up -d
