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
    public class ProfesorRepository : IRepository<ProfesorDto>, IPerson<ProfesorDto>
    {
        private readonly AppDbContext _context;
        public ProfesorRepository(AppDbContext context)
        {
            _context = context;
        }

        private async Task<bool> ExistDniAsync(string dni) => _context.Profesores.Any(c => c.Dni == dni);
        public async Task<GeneralResponse> AddAsync(ProfesorDto model)
        {
            if (await ExistDniAsync(model.Dni))
                return new GeneralResponse(false, "El D.N.I ya existe");
            try
            {
                var profesorAdded = new Profesor()
                {
                    Nombre = model.Nombre,
                    Dni = model.Dni,
                    Especialidad = model.Especialidad,
                    Cursos = new List<Curso>()
                };
                await _context.Profesores.AddAsync(profesorAdded);
                await _context.SaveChangesAsync();
                return new GeneralResponse(true, "Registro agregado con éxito");
            }
            catch (Exception ex)
            {
                return new GeneralResponse(false, ex.Message);
            }
        }

        public async Task<GeneralResponse> DeleteAsync(int id)
        {
            var entity = await _context.Profesores.FindAsync(id);            
            if (entity == null)
                return new GeneralResponse(false, "error el id no existe");
            if (entity != null)
            {
                _context.Profesores.Remove(entity);
                await _context.SaveChangesAsync();
                return new GeneralResponse(true, "Registro Eliminado");
            }
            return new GeneralResponse(false, "Error al eliminar");
        }

        public async Task<IEnumerable<ProfesorDto>> GetAllAsync()
        {

            //var profesores = await _context.Profesores.Include(x=> x.Cursos).ToListAsync();
            var profesores = await _context.Profesores.ToListAsync();
            return profesores.ToDto();

        }

        public async Task<ProfesorDto> GetByDniAsync(string dni) 
        {
            var profesor = await _context.Profesores.Where(p => p.Dni == dni).FirstOrDefaultAsync();
            if (profesor != null)
                return profesor.ToDto();
            return null;
        }

        public async Task<ProfesorDto> GetByIdAsync(int id) 
        {
            var profesor = await _context.Profesores.FindAsync(id);
            if (profesor != null)
                return profesor.ToDto();
            return null;
        }

        public async Task<GeneralResponse> UpdateAsync(ProfesorDto model)
        {
            try
            {
                var profesor = await _context.Profesores.FindAsync(model.Id);
                if (profesor == null)
                    return new GeneralResponse(false, "error el id no existe");


                var estudianteExistente = await _context.Profesores
                  .FirstOrDefaultAsync(e => e.Dni == model.Dni && e.Id != model.Id);
                if (estudianteExistente != null)
                    return new GeneralResponse(false, "Ya existe un profesor con el mismo DNI.");


                profesor.Dni = model.Dni;
                profesor.Nombre = model.Nombre;
                profesor.Especialidad = model.Especialidad;

                _ = _context.Profesores.Update(profesor);
                await _context.SaveChangesAsync();
                return new GeneralResponse(true, "Registro actualizado");
            }
            catch (Exception ex)
            {
                return new GeneralResponse(false, ex.Message);
            }
        }

        
    }
}
