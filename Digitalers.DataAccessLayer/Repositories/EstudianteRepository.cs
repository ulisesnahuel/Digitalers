using Digitalers.DataAccessLayer.Contracts;
using Digitalers.DataAccessLayer.Mappers;
using Digitalers.DataAccessLayer.Models;
using Digitalers.DomainLayer.Entities;
using Digitalers.Shared.Dtos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Digitalers.DataAccessLayer.Repositories
{
    public class EstudianteRepository: IRepository<EstudianteDto>, IPerson<EstudianteDto>
    {
        private readonly AppDbContext _context;
        public EstudianteRepository(AppDbContext context)
        {
            _context = context;
        }

        private async Task<bool> ExistDniAsync(string dni) => _context.Estudiantes.Any(c => c.Dni == dni);
        public async Task<GeneralResponse> AddAsync(EstudianteDto entity)
        {
            if (await ExistDniAsync(entity.Dni))
                return new GeneralResponse(false, "El D.N.I ya existe");
            try
            {
                var estudianteAdded = new Estudiante()
                {
                    Dni = entity.Dni,
                    Nombre = entity.Nombre,
                    Edad = entity.Edad,
                    Cursos = new List<Curso>()
                };
                await _context.Estudiantes.AddAsync(estudianteAdded);
                await _context.SaveChangesAsync();
                return new GeneralResponse(true, "Estudiante agregado con éxito");
            }
            catch (Exception ex)
            {
                return new GeneralResponse(false, ex.Message);
            }
        }

        public async Task<GeneralResponse> DeleteAsync(int id)
        {
            var entity = await _context.Estudiantes.FindAsync(id);
            if (entity != null)
            {
                _context.Estudiantes.Remove(entity);
                await _context.SaveChangesAsync();
                return new GeneralResponse(true, "Estudiante Eliminado");
            }
            return new GeneralResponse(false, "Error al eliminar");
        }

        public async Task<IEnumerable<EstudianteDto>> GetAllAsync()
        {
            var estudiantes = await _context.Estudiantes.Include(c => c.Cursos).ToListAsync();
            return (estudiantes.ToDto());
        }

        public async Task<EstudianteDto> GetByDniAsync(string dni)
        {
            var estudiante = await _context.Estudiantes.Where(p => p.Dni == dni).Include(c => c.Cursos).FirstOrDefaultAsync();
            if (estudiante != null)
                return estudiante.ToDto();
            return null;
        }

        public async Task<EstudianteDto> GetByIdAsync(int id)
        {
            var estudiante = await _context.Estudiantes.Where(p => p.Id ==id).Include(c => c.Cursos).FirstOrDefaultAsync();
            if (estudiante != null)
                return estudiante.ToDto();
            return null;
        }

        public async Task<GeneralResponse> UpdateAsync(EstudianteDto model)
        {
            try
            {
                var estudiante = await _context.Estudiantes.FindAsync(model.Id);
                if (estudiante == null)
                    return new GeneralResponse(false, "error el id no existe");

                var estudianteExistente = await _context.Estudiantes
                    .FirstOrDefaultAsync(e => e.Dni == model.Dni && e.Id != model.Id); 
                if (estudianteExistente != null)
                    return new GeneralResponse(false, "Ya existe un estudiante con el mismo DNI.");



                estudiante.Dni = model.Dni;
                estudiante.Nombre = model.Nombre;
                estudiante.Edad = model.Edad;

                _= _context.Estudiantes.Update(estudiante);
                await _context.SaveChangesAsync();
                return new GeneralResponse(true, "Estudiante actualizado");
            }
            catch (Exception ex)
            {
                return new GeneralResponse(false, ex.Message);
            }
        }
    }
}
