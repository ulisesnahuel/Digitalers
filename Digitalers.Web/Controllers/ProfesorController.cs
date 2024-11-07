using Digitalers.Shared.Dtos;
using Digitalers.Web.Models;
using Digitalers.Web.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace Digitalers.Web.Controllers
{
    public class ProfesorController : Controller
    {
        
            private readonly IProfesorService _profesorService;

            public ProfesorController(IProfesorService profesorService)
            {
            _profesorService = profesorService;
            }

            public async Task<IActionResult> Index()
            {
                var profesorDto = await _profesorService.GetProfesoresAsync();

                var profesor = profesorDto.Select(c => new ProfesorViewModel
                {
                    Id = c.Id,
                    Nombre = c.Nombre,
                    Dni = c.Dni,
                    Especialidad = c.Especialidad,  
                    

                    
                }).ToList();

                return View(profesor);
            }

            [HttpGet]
            public IActionResult Create()
            {
                return View(new ProfesorViewModel());
            }
            [HttpPost]
            public async Task<IActionResult> Create(ProfesorViewModel model)
            {
                if (!ModelState.IsValid)
                {
                    ModelState.AddModelError("", "Complete los datos");

                    return View(model);
                }

                var profesor = new ProfesorDto
                {
                    Id = model.Id,
                    Nombre = model.Nombre,
                    Dni = model.Dni,
                    Especialidad= model.Especialidad,

                };

                var result = await _profesorService.AddProfesorAsync(profesor);
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
                var profesorDto = await _profesorService.GetByIdAsync(id);
                if (profesorDto == null)
                {
                    return NotFound();
                }

                var model = new ProfesorViewModel
                {
                    Id = profesorDto.Id,
                    Nombre = profesorDto.Nombre,
                    Dni = profesorDto.Dni,
                    Especialidad = profesorDto.Especialidad
                };

                return View(model);
            }

            [HttpPost]
            public async Task<IActionResult> Edit(ProfesorViewModel model)
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }

                var result = await _profesorService.UpdateProfesorAsync(new ProfesorDto
                {
                    Id = model.Id,
                    Nombre = model.Nombre,
                    Dni = model.Dni,
                    Especialidad =model.Especialidad
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
                var profesorDto = await _profesorService.GetByIdAsync(id);
                if (profesorDto == null)
                {
                    return NotFound();
                }
                var model = new ProfesorViewModel
                {
                    Id = profesorDto.Id,
                    Nombre = profesorDto.Nombre,
                    Dni = profesorDto.Dni,
                    Especialidad = profesorDto.Especialidad
                };

                return View(model);
            }

            [HttpPost]
            [ActionName("DeleteConfirmed")] 
            public async Task<IActionResult> DeleteConfirmed(int id)
            {
                var result = await _profesorService.DeleteProfesorAsync(id);
                if (result.Flag)
                {
                    return RedirectToAction("Index");
                }

                ModelState.AddModelError("", result.Message);
                var profesor = await _profesorService.GetByIdAsync(id);

                var model = new ProfesorViewModel
                {
                    Id = profesor.Id,
                    Nombre = profesor.Nombre,
                    Dni = profesor.Dni,
                    Especialidad = profesor.Especialidad
                };

                return View("Delete", model); 
            }
        
    }
}
