using Digitalers.DataAccessLayer.Contracts;
using Digitalers.DataAccessLayer.Mappers;
using Digitalers.DataAccessLayer.Models;
using Digitalers.DomainLayer.Entities;
using Digitalers.Shared.Dtos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Digitalers.DataAccessLayer.Repositories
{
    public class CursoRepository : IRepository<CursoDto>
    {
        private readonly AppDbContext _context;
        public CursoRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<GeneralResponse> AddAsync(CursoDto entity)
        {
            try
            {
                var cursoAdded = new Curso()
                {
                    Duracion = entity.Duracion,
                    Nombre = entity.Nombre,
                    ProfesorId = entity.ProfesorId,
                  


                };

                if (entity.Estudiantes?.Any() == true)
                {
                    var estudiantesIds = entity.Estudiantes.Select(e => e.Id).ToList();
                    var estudiantes = await _context.Estudiantes
                        .Where(e => estudiantesIds.Contains(e.Id))
                        .ToListAsync();

                    cursoAdded.Estudiantes!.AddRange(estudiantes);

                }

                await _context.Cursos.AddAsync(cursoAdded);
                await _context.SaveChangesAsync();
                return new GeneralResponse(true, "Curso agregado con éxito");
            }
            catch (Exception ex)
            {
                return new GeneralResponse(false, ex.Message);
            }
        }
        public async Task<GeneralResponse> UpdateAsync(CursoDto model)
        {
            try
            {
                var curso = await _context.Cursos
                    .Include(c => c.Estudiantes) 
                    .FirstOrDefaultAsync(c => c.Id == model.Id);

                if (curso == null)
                    return new GeneralResponse(false, "Error: El curso con el ID especificado no existe");

                var estudiantesSeleccionadosIds = model.Estudiantes.Select(e => e.Id).ToList();
                var estudiantesAEliminar = curso.Estudiantes
                    .Where(e => !estudiantesSeleccionadosIds.Contains(e.Id)) 
                    .ToList();

                foreach (var estudiante in estudiantesAEliminar)
                {
                    curso.Estudiantes.Remove(estudiante); 
                }

                var estudiantesAAgregar = model.Estudiantes
                    .Where(e => !curso.Estudiantes.Any(est => est.Id == e.Id)) 
                    .ToList();

                foreach (var estudiante in estudiantesAAgregar)
                {
                    curso.Estudiantes.Add(new Estudiante
                    {
                        Id = estudiante.Id,
                        Nombre = estudiante.Nombre,
                        Edad = estudiante.Edad,
                        Dni = estudiante.Dni,
                    });
                }

                curso.Nombre = model.Nombre;
                curso.Duracion = model.Duracion;
                curso.ProfesorId = model.ProfesorId;

                await _context.SaveChangesAsync();

                return new GeneralResponse(true, "Curso actualizado exitosamente");
            }
            catch (Exception ex)
            {
                return new GeneralResponse(false, ex.Message);
            }
        }

        

        public async Task<GeneralResponse> DeleteAsync(int id)
        {
            var entity = await _context.Cursos.FindAsync(id);
            if (entity != null)
            {
                _context.Cursos.Remove(entity);
                await _context.SaveChangesAsync();
                return new GeneralResponse(true, "Curso Eliminado");
            }
            return new GeneralResponse(false, "Error al eliminar");
        }

        public async Task<IEnumerable<CursoDto>> GetAllAsync()
        {
            var cursos = await _context.Cursos
                .Include(x => x.Estudiantes)
                .Include(x => x.Profesor)
                .ToListAsync();
            return (cursos.ToDto());
        }

        public async Task<CursoDto> GetByIdAsync(int id)
        {
            var curso = await _context.Cursos
                .Include(c => c.Estudiantes)
                .Include(x => x.Profesor)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (curso != null)
                return curso.ToDto();

            return null;

        }

        
    }
}
