using AutoMapper;
using LeadsAPI.DTOs;
using LeadsAPI.Entidades;
using LeadsAPI.Helpers;
using LeadsAPI.Datos;
using LeadsAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace LeadsAPI.Controllers
{
    [ApiController]
    [Route("api/leads")]
    public class LeadsController : ControllerBase
    {
        private readonly WorkshopService _workshopService;
        private readonly RepositorioLead _repository;
        private readonly IMapper mapper;
        public LeadsController(WorkshopService workshopService, RepositorioLead repository, IMapper mapper)
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
                    return BadRequest(new { error = "Invalido PlaceId. El taller no esta activo o no existe." });
                }

                // mapeo DTO a Entidad Lead y guardado
                Lead lead = mapper.Map<Lead>(leadCreacionDTO);

                // valido duplicidad de datos y guardo
                if (!_repository.Save(lead, out string message))
                {
                    return BadRequest(new { error = message }); 
                }

                return Created(string.Empty, new
                {
                    message,
                    lead
                });

            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Ocurrio un error inesperado en la API. " + ex });
            }
        }

        [HttpGet]
        public ActionResult<IEnumerable<LeadDTO>> Get()
        {
            var leads = _repository.GetAll();
            var leadDTO = mapper.Map<IEnumerable<LeadDTO>>(leads);
            return Ok(leadDTO);
        }
    }
}
