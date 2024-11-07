using Digitalers.DataAccessLayer.Contracts;
using Digitalers.DomainLayer.Entities;
using Digitalers.Shared.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Digitalers.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfesorController : ControllerBase
    {
        private readonly IRepository<ProfesorDto> _profesorRepository;
        private readonly IPerson<ProfesorDto> _personRepository;

        public ProfesorController(IRepository<ProfesorDto> profesorRepository, IPerson<ProfesorDto> personRepository)
        {
            _profesorRepository = profesorRepository;
            _personRepository = personRepository;
        }

        //[HttpGet]
        //[Route("profesos")]

        //public IActionResult Get()
        //{
        //    return Ok("Funciona");
        //}

        [HttpGet]

        public async Task<ActionResult<IEnumerable<ProfesorDto>>> GetAllProfesores()
        {
            var profesores = await _profesorRepository.GetAllAsync();
            return Ok(profesores);
        }

        [HttpGet("{id:int}")]
        //[Route("/profesor/{id:int}")]
        public async Task<ActionResult<ProfesorDto>> GetProfesor(int id)
        {
            var profesor = await _profesorRepository.GetByIdAsync(id);
            if (profesor is null)
            {
                return NotFound();
            }
            return Ok(profesor);
        }
        [HttpGet("dni/{dni}")]        
        public async Task<ActionResult<ProfesorDto>> GetProfesorByDni(string dni)
        {
            var profesor = await _personRepository.GetByDniAsync(dni);
            if (profesor is null)
            {
                return NotFound();
            }
            return Ok(profesor);
        }

        [HttpPost]
        public async Task<ActionResult<GeneralResponse>> AddProfesor(ProfesorDto profesor)
        {
            var profesorAdded = await _profesorRepository.AddAsync(profesor);
            if (!profesorAdded.Flag)
            {
                return BadRequest(profesorAdded);
            }
            return Ok(profesorAdded);

        }

        [HttpPut]
        public async Task<ActionResult<GeneralResponse>> UpdateProfesor(ProfesorDto profesor)
        {
            var profesorUpdated = await _profesorRepository.UpdateAsync(profesor);
            if (!profesorUpdated.Flag)
            {
                return BadRequest(profesorUpdated);
            }
            return Ok(profesorUpdated);
        }

        [HttpDelete]
        public async Task<ActionResult<GeneralResponse>> DeleteProfesor(int id)
        {
            var profesorDeleted = await _profesorRepository.DeleteAsync(id);
            if (!profesorDeleted.Flag)
            {
                return BadRequest(profesorDeleted);
            }
            return Ok(profesorDeleted);
        }
    }
}
