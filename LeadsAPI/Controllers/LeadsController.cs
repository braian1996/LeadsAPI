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
        public async Task<IActionResult> Post([FromBody] LeadDTO leadCreacionDTO)
        {
            try
            {
                //llamo a la clase Validator para verificar requeridos mediente el uso FluentValidation
                LeadValidator validator = new LeadValidator();
                var validationResult = validator.Validate(leadCreacionDTO);
                //datos requeridos y en el formato determinado
                if (!validationResult.IsValid)
                {
                    return BadRequest(validationResult.Errors.Select(e => e.ErrorMessage));
                }
                //llamo a la API externa para obtener los talleres activos y valida si el taller enviado esta dentro de la lista
                HashSet<int> activeWorkshops = await _workshopService.GetActiveWorkshops();
                if (!activeWorkshops.Contains(leadCreacionDTO.PlaceId))
                {
                    return BadRequest(new { error = "Invalido place_id. El place no esta activo." });
                }
                //hago el mapeo del DTO a la entidad Lead y guardo
                Lead lead = mapper.Map<Lead>(leadCreacionDTO);
                _repository.Save(lead);

                return Created(string.Empty, new
                {
                    message = "Su turno fue creado con éxito",
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Ocurrió un error inesperado en la API." + ex });
            }
        }
    }
}
