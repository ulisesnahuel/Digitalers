using Digitalers.Shared.Dtos;
using Digitalers.Web.Models;
using Digitalers.Web.Services.Contracts;
using Digitalers.Web.Services.Implementation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Digitalers.Web.Controllers
{
    public class CursoController : Controller
    {
        private readonly ICursoService _cursoService;
        private readonly IEstudianteService _estudianteService;
        private readonly IProfesorService _profesorService;

        public CursoController(ICursoService cursoService, IEstudianteService estudianteService, IProfesorService profesorService)
        {
            _cursoService = cursoService;
            _estudianteService = estudianteService;
            _profesorService = profesorService; 
        }
        public async Task<IActionResult> Index()
        {
            var cursoDto = await _cursoService.GetCursosAsync();

            var cursoViewModel = cursoDto.Select(c => new CursoViewModel
            {
                Id = c.Id,
                Nombre = c.Nombre,
                Duracion = c.Duracion,
                ProfesorId = c.ProfesorId,
                ProfesorNombre = c.ProfesorNombre,        
                Estudiantes = c.Estudiantes.Select(e => new EstudianteDto
                {
                    Id = e.Id,
                    Nombre = e.Nombre,
                    Dni = e.Dni,
                    Edad = e.Edad
                }).ToList()


            }).ToList();

            return View(cursoViewModel);
        }

        public async Task<IActionResult> Details(int id)
        {
            var cursoDto = await _cursoService.GetByIdAsync(id); 

            if (cursoDto == null)
            {
                return NotFound();
            }

            var cursoViewModel = new CursoViewModel
            {
                Id = cursoDto.Id,
                Nombre = cursoDto.Nombre,
                Duracion = cursoDto.Duracion,
                ProfesorId = cursoDto.ProfesorId,
                ProfesorNombre = cursoDto.ProfesorNombre,
                Estudiantes = cursoDto.Estudiantes.Select(e => new EstudianteDto
                {
                    Id = e.Id,
                    Nombre = e.Nombre,
                    Dni = e.Dni,
                    Edad = e.Edad
                }).ToList()
            };

            return View(cursoViewModel);
        }

        public async Task<IActionResult> Create()
        {
            ViewBag.Profesores = new SelectList(await _profesorService.GetProfesoresAsync(), "Id", "Nombre");
            var estudiantesDto = await _estudianteService.GetEstudiantesAsync();
            var estudiantesViewModel = estudiantesDto.Select(e => new EstudianteViewModel
            {
                Id = e.Id,
                Nombre = e.Nombre,
                Dni = e.Dni,
                Edad = e.Edad
            }).ToList();

            ViewBag.Estudiantes = estudiantesViewModel; 
            return View();

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CursoViewModel model, int[] EstudiantesSeleccionados)
        {
            if (ModelState.IsValid)
            {
                model.Estudiantes = EstudiantesSeleccionados
            .Select(id => new EstudianteDto { Id = id })
            .ToList();


                var curso = new CursoDto
                {
                    Id = model.Id,
                    Nombre = model.Nombre,
                    Duracion = model.Duracion,
                    ProfesorId = model.ProfesorId,
                    Estudiantes = model.Estudiantes,
                };


                   


                var result = await _cursoService.AddCursoAsync(curso);
                if (!result.Flag)
                {

                }
                return RedirectToAction("Index");
            }

            ViewBag.Profesores = new SelectList(await _profesorService.GetProfesoresAsync(), "Id", "Nombre");
            ViewBag.Estudiantes = await _estudianteService.GetEstudiantesAsync();
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var curso = await _cursoService.GetByIdAsync(id);
            var profesores = await _profesorService.GetProfesoresAsync();
            var estudiantes = await _estudianteService.GetEstudiantesAsync();

            var viewModel = new EditCursoViewModel
            {
                Id = curso.Id,
                Nombre = curso.Nombre,
                Duracion = curso.Duracion,
                ProfesorId = curso.ProfesorId,
                Profesores = profesores.Select(p => new ProfesorViewModel
                {
                    Id = p.Id,
                    Nombre = p.Nombre
                }).ToList(),
                EstudiantesDisponibles = estudiantes.Select(e => new EstudianteViewModel
                {
                    Id = e.Id,
                    Nombre = e.Nombre,
                    Dni = e.Dni,
                    Edad = e.Edad
                }).ToList(),
                EstudiantesSeleccionados = curso.Estudiantes.Select(e => e.Id).ToList()

            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditCursoViewModel model)
        {
            if (!ModelState.IsValid)
            {
                // Recargar los datos de los profesores y estudiantes
                var profesores = await _profesorService.GetProfesoresAsync();
                var estudiantes1 = await _estudianteService.GetEstudiantesAsync();

                model.Profesores = profesores.Select(p => new ProfesorViewModel
                {
                    Id = p.Id,
                    Nombre = p.Nombre
                }).ToList();

                model.EstudiantesDisponibles = estudiantes1.Select(e => new EstudianteViewModel
                {
                    Id = e.Id,
                    Nombre = e.Nombre,
                    Dni = e.Dni,
                    Edad = e.Edad
                }).ToList();

                return View(model);
            }

            var estudiantes = await _estudianteService.GetEstudiantesAsync();

            var estudiantesSeleccionados = estudiantes.Where(e => model.EstudiantesSeleccionados
            .Contains(e.Id))
                .Select(e => new EstudianteDto
                {
                    Id = e.Id,
                    Nombre = e.Nombre,
                    Edad = e.Edad,
                    Dni = e.Dni
                })
                        .ToList();

            var curso = new CursoDto
            {
                Id = model.Id,
                Nombre = model.Nombre,
                Duracion = model.Duracion,
                ProfesorId = model.ProfesorId,
                Estudiantes = estudiantesSeleccionados
                //Estudiantes = model.EstudiantesSeleccionados.Select(id => new EstudianteDto
                //{
                //    Id = id,


                //}).ToList()
            };

            await _cursoService.UpdateCursoAsync(curso);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int id)
        {
            var curso = await _cursoService.GetByIdAsync(id);

            if (curso == null)
            {
                return NotFound();
            }

            var cursoViewModel = new CursoViewModel
            {
                Id = curso.Id,
                Nombre = curso.Nombre,
                Duracion = curso.Duracion,
                ProfesorNombre = curso.ProfesorNombre,
                ProfesorId = curso.ProfesorId,
                Estudiantes = curso.Estudiantes
            };

            return View(cursoViewModel);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var result = await _cursoService.DeleteCursoAsync(id);

            if (result.Flag)
            {
                return RedirectToAction(nameof(Index)); 
            }
            else
            {
                return View();
            }
        }
    }
}
