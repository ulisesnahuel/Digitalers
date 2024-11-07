using Digitalers.DataAccessLayer.Contracts;
using Digitalers.DomainLayer.Entities;
using Digitalers.Shared.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Digitalers.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EstudianteController : ControllerBase
    {
        private readonly IRepository<EstudianteDto> _EstudianteRepository;
        private readonly IPerson<EstudianteDto> _personRepository;

        public EstudianteController(IRepository<EstudianteDto> estudianteRepository, IPerson<EstudianteDto> personRepository)
        {
            _EstudianteRepository = estudianteRepository;
            _personRepository = personRepository;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<EstudianteDto>>> GetAllEstudiantes()
        {
            var estudiantes = await _EstudianteRepository.GetAllAsync();
            return Ok(estudiantes);
        }

        [HttpGet("{id:int}")]
        //[Route("/estudiante/{id:int}")]
        public async Task<ActionResult<EstudianteDto>> GetEstudiante(int id)
        {
            var estudiante = await _EstudianteRepository.GetByIdAsync(id);
            if (estudiante is null)
            {
                return NotFound();
            }
            return Ok(estudiante);
        }
        [HttpGet("dni/{dni}")]
        //[Route("/estudiante/{id:int}")]
        public async Task<ActionResult<EstudianteDto>> GetEstudianteByDni(string dni)
        {
            var estudiante = await _personRepository.GetByDniAsync(dni);
            if (estudiante is null)
            {
                return NotFound();
            }
            return Ok(estudiante);
        }

        [HttpPost]
        public async Task<ActionResult<GeneralResponse>> AddEstudiante(EstudianteDto estudiante)
        {
            var estudianteAdded = await _EstudianteRepository.AddAsync(estudiante);
            if (!estudianteAdded.Flag)
            {
                return BadRequest(estudianteAdded);
            }
            return Ok(estudianteAdded);

        }

        [HttpPut]
        public async Task<ActionResult<GeneralResponse>> UpdateEstudiante(EstudianteDto estudiante)
        {
            var estudianteUpdated = await _EstudianteRepository.UpdateAsync(estudiante);
            if (!estudianteUpdated.Flag)
            {
                return BadRequest(estudianteUpdated);
            }
            return Ok(estudianteUpdated);
        }

        [HttpDelete]
        public async Task<ActionResult<GeneralResponse>> DeleteEstudiante(int id)
        {
            var estudianteDeleted = await _EstudianteRepository.DeleteAsync(id);
            if (!estudianteDeleted.Flag)
            {
                return BadRequest(estudianteDeleted);
            }
            return Ok(estudianteDeleted);
        }
    }
}
