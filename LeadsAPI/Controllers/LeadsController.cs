using AutoMapper;
using FluentValidation;
using LeadsAPI.DTOs;
using LeadsAPI.Entidades;
using LeadsAPI.Helpers;
using LeadsAPI.Repositorios;
using LeadsAPI.Services;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace LeadsAPI.Controllers
{
    [ApiController]
    [Route("api/leads")]
    public class LeadsController : ControllerBase
    {
        private readonly WorkshopService _workshopService;
        private readonly LeadRepository _repository;
        private readonly IMapper mapper;
        public LeadsController(WorkshopService workshopService, LeadRepository repository, IMapper mapper)
        {
            _workshopService = workshopService;
            _repository = repository;
            this.mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult<LeadDTO>> Post([FromBody] LeadDTO leadCreacionDTO)
        {
            try
            {
                //  validacion del DTO
                LeadValidator validator = new LeadValidator();
                var validationResult = validator.Validate(leadCreacionDTO);
                if (!validationResult.IsValid)
                {
                    return BadRequest(validationResult.Errors.Select(e => e.ErrorMessage));
                }

                // validacion del place_id contra la API externa
                HashSet<int> activeWorkshops = await _workshopService.GetActiveWorkshops();
                if (!activeWorkshops.Contains(leadCreacionDTO.PlaceId))
                {
                    return BadRequest(new { error = "Invalido place_id. El place no esta activo." });
                }

                // mapeo DTO a Entidad Lead y guardado
                Lead lead = mapper.Map<Lead>(leadCreacionDTO);
                _repository.Save(lead);

                // devolucion del turno creado + mensaje (respuesta 201 Created)
                return Created(string.Empty, new
                {
                    message = "Su turno fue creado con éxito",
                    lead
                });

            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Ocurrió un error inesperado en la API. " + ex });
            }
        }
    }
}
