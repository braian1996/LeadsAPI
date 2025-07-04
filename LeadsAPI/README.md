# LeadsAPI - Web API para Gestion de Turnos en Talleres

Este proyecto es una Web API desarrollada con **.NET Core 9**, enfocada en la creacion de turnos para talleres.

---

## Detalle General

- Permite registrar turnos (leads) a talleres.
- Valida que el taller exista y esté activo mediante una API externa.
- Expone un endpoint para obtener todos los turnos registrados.

---

## Funciones

### Crear Turno (POST /api/leads)
- Valida todos los datos obligatorios: taller, fecha del turno, tipo de servicio, datos de contacto y patente (si se envía vehículo).
- Consulta la API externa para validar que el taller exista y esté activo.
- Guarda el turno en memoria.
- Devuelve el turno creado junto con un mensaje de exito.
- A continuacion dejo ejemplos de request exitosos:


```
201 Created
{
  "place_id": 2,
  "appointment_at": "2025-12-10 16:00",
  "service_type": "rotacion_neumaticos",
  "contact": {
    "name": "braian wenger",
    "email": "bwenger@gmail.com",
    "phone": "156578016"
  },
  "vehicle": {
    "make": "Toyota",
    "model": "corolla",
    "year": 2020,
    "license_plate": "igc138"
  }
}
```
```
201 Created
{
  "place_id": 5,
  "appointment_at": "2025-03-10 18:00",
  "service_type": "otro",
  "contact": {
    "name": "ramiro peron",
    "email": "rp@gmail.com",
    "phone": "155654321"
  }
}
```


### Obtener Todos los Turnos (GET /api/leads)
- Devuelve la lista completa de turnos registrados.

---

## Tecnologias

- **.NET Core 9**
- **C#**
- **FluentValidation**: Para validaciones robustas.
- **AutoMapper**: Para mapeo entre DTOs y entidades.
- **IMemoryCache**: Para guardar los talleres activos y evitar llamadas repetidas a la API externa.
- **IHttpClientFactory**: Para consumir APIs externas.

---

## Configuracion

La URL y credenciales de la API externa se configuran desde `appsettings.json`.

---
**Autor: Braian Wenger** 