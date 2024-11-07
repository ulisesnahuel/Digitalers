using Digitalers.DataAccessLayer.Contracts;
using Digitalers.DomainLayer.Entities;
using Digitalers.Shared.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Digitalers.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CursoController : ControllerBase
    {
        private readonly IRepository<CursoDto> _cursoRepository;

        public CursoController( IRepository<CursoDto> cursoRepository)
        {
            _cursoRepository = cursoRepository;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<CursoDto>>> GetAllCursos()
        {
            var cursos = await _cursoRepository.GetAllAsync();
            return Ok(cursos);
        }

        [HttpGet("{id:int}")]
        //[Route("/curso/{id:int}")]
        public async Task<ActionResult<CursoDto>> GetCurso(int id)
        {
            var curso = await _cursoRepository.GetByIdAsync(id);
            if (curso is null)
            {
                return NotFound();
            }
            return Ok(curso);
        }

        [HttpPost]
        public async Task<ActionResult<GeneralResponse>> AddCurso(CursoDto curso)
        {
            var cursoAdded = await _cursoRepository.AddAsync(curso);
            if (!cursoAdded.Flag)
            {
                return BadRequest(cursoAdded);
            }
            return Ok(cursoAdded);

        }

        [HttpPut]
        public async Task<ActionResult<GeneralResponse>> UpdateCurso(CursoDto curso)
        {
            var cursoUpdated = await _cursoRepository.UpdateAsync(curso);
            if (!cursoUpdated.Flag)
            {
               return BadRequest(cursoUpdated);
            }
            return Ok(cursoUpdated);
        }

        [HttpDelete]
        public async Task<ActionResult<GeneralResponse>> DeleteCurso(int id)
        {
            var cursoDeleted = await _cursoRepository.DeleteAsync(id);
            if (!cursoDeleted.Flag)
            {
                return BadRequest(cursoDeleted);
            }
            return Ok(cursoDeleted);    
        }

    }
}
