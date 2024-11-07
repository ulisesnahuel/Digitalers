using Digitalers.Shared.Dtos;
using Digitalers.Web.Services;
using Microsoft.AspNetCore.Mvc;
using Digitalers.Web.Models;
using Digitalers.Web.Services.Contracts;

namespace Digitalers.Web.Controllers
{
    public class EstudianteController : Controller
    {
        private readonly IEstudianteService _estudianteService;

        public EstudianteController(IEstudianteService estudianteService)
        {
            _estudianteService = estudianteService;
        }

        public async Task<IActionResult> Index() 
        {
            var estudianteDtos= await _estudianteService.GetEstudiantesAsync();

            var estudianteViewModels = estudianteDtos.Select(c => new EstudianteViewModel
            {
                Id = c.Id,
                Nombre = c.Nombre,
                Dni = c.Dni,
                Edad = c.Edad,

            }).ToList();

            return View(estudianteViewModels);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(new EstudianteViewModel());
        }
        [HttpPost]
        public async Task<IActionResult> Create(EstudianteViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var estudiante = new EstudianteDto
            {
                Id = model.Id,
                Nombre = model.Nombre,
                Dni = model.Dni,
                Edad = model.Edad,

            };

            var result = await  _estudianteService.AddEstudianteAsync(estudiante);
            if (!result.Flag)
            {
                model.Error = result.Message;
                return View(model);
            }
            return RedirectToAction("Index");   

        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var estudianteDto = await _estudianteService.GetByIdAsync(id);
            if (estudianteDto == null)
            {
                return NotFound();
            }

            var model = new EstudianteViewModel
            {
                Id = estudianteDto.Id,
                Nombre = estudianteDto.Nombre,
                Dni = estudianteDto.Dni,
                Edad = estudianteDto.Edad
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EstudianteViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Complete todos datos");

                return View(model);
            }

            

            var result = await _estudianteService.UpdateEstudianteAsync(new EstudianteDto
            {
                Id = model.Id,
                Nombre = model.Nombre,
                Dni = model.Dni,
                Edad = model.Edad
            });

            if (result.Flag)
            {
                return RedirectToAction("Index");
            }

            model.Error = result.Message;
            return View(model);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var estudianteDto = await _estudianteService.GetByIdAsync(id);
            if (estudianteDto == null)
            {
                return NotFound();
            }
            var model = new EstudianteViewModel
            {
                Id = estudianteDto.Id,
                Nombre = estudianteDto.Nombre,
                Dni = estudianteDto.Dni,
                Edad = estudianteDto.Edad
            };

            return View(model);
        }

        [HttpPost]
        [ActionName("DeleteConfirmed")] 
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var result = await _estudianteService.DeleteEstudianteAsync(id);
            if (result.Flag)
            {
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", result.Message);
            var estudiante = await _estudianteService.GetByIdAsync(id);

            var model = new EstudianteViewModel
            {
                Id = estudiante.Id,
                Nombre = estudiante.Nombre,
                Dni = estudiante.Dni,
                Edad = estudiante.Edad
            };

            return View("Delete", model); 
        }
    }
}
