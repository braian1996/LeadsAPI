# LeadsAPI - Web API para Gestion de Turnos en Talleres

Este proyecto es una Web API desarrollada con **.NET Core 9**.

---

## Detalle General

- Permite registrar turnos a talleres.
- Valida que el taller exista y esté activo mediante una API externa.
- Permite obtener todos los turnos registrados.

---

## Metodos que expone la API

### Crear Turno (POST)
- Valida todos los datos obligatorios: taller, fecha del turno, tipo de servicio, datos de contacto y patente (si se envia vehiculo).
- Consulta la API externa para validar que el taller exista y este activo.
- Guarda el turno en memoria.
- Devuelve el turno creado junto con un mensaje de exito.
- A continuacion dejo ejemplos de request exitosos:


```
https://localhost:7240/api/leads

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


### Obtener Todos los Turnos (GET)
- Devuelve la lista completa de turnos registrados.

---

## Tecnologias

- **.NET Core 9**
- **C#**
- **FluentValidation**: Para validaciones de campos.
- **AutoMapper**: Para mapeo entre DTOs y entidades.
- **IMemoryCache**: Para guardar los talleres activos y evitar llamar varias veces a la API externa.
- **IHttpClientFactory**: Para consumir APIs externas.

---

## Configuracion

La URL y credenciales de la API externa se configuran desde `appsettings.json`.

---
**Autor: Braian Wenger** 