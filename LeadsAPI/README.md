# LeadsAPI - Web API para Gestión de Turnos en Talleres

Este proyecto es una Web API desarrollada con **.NET Core 9**, enfocada en la creación turnos (leads) para talleres.

---

## Descripción General

- Permite registrar turnos (leads) a talleres.
- Valida que el taller (PlaceId) exista y esté activo mediante una API externa.
- Expone un endpoint para obtener todos los turnos registrados.
- La aplicación está preparada para soportar cambios futuros.

---

## Funcionalidades Principales

### Crear Turno (POST /api/leads)
- Valida todos los datos obligatorios: taller, fecha del turno, tipo de servicio, datos de contacto y patente (si se envía vehículo).
- Consulta la API externa para validar que el taller exista y esté activo.
- Guarda el turno en memoria.
- Devuelve el turno creado junto con un mensaje de éxito.


### Obtener Todos los Turnos (GET /api/leads)
- Devuelve la lista completa de turnos registrados durante la ejecución de la aplicación.

---

## Tecnologías Usadas

- **.NET Core 9**
- **C#**
- **FluentValidation** → Para validaciones robustas.
- **AutoMapper** → Para mapeo entre DTOs y entidades.
- **IMemoryCache** → Para cachear los talleres activos y evitar llamadas repetidas a la API externa.
- **IHttpClientFactory** → Para consumir APIs externas de forma segura y eficiente.

---

## Configuración

Este proyecto no tiene datos sensibles hardcodeados. La URL y credenciales de la API externa se configuran fácilmente desde `appsettings.json`.

---
**Autor: Braian Wenger** 