using Digitalers.DataAccessLayer.Contracts;
using Digitalers.DataAccessLayer.Models;
using Digitalers.Shared.Dtos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Digitalers.DataAccessLayer.Repositories
{
    public class GenericRepository<T> : IRepository<T> where T : class
    {
        private readonly AppDbContext _context;
        private readonly DbSet<T> _dbSet;   

        public GenericRepository( AppDbContext context )
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        private async Task<bool> FindDniAsync(T entity)
        {
            var propertyInfo = typeof(T).GetProperty("Dni");
            if (propertyInfo == null)
            {
                return false;
            }

            var dniValue = propertyInfo.GetValue(entity)?.ToString();

            if (dniValue == null)
            {
                return false;
            }

            var exists = await _dbSet.AnyAsync(e =>
                propertyInfo.GetValue(e) != null &&
                propertyInfo.GetValue(e).ToString() == dniValue
            );

            return !exists;
        }

        public async Task<IEnumerable<T>> GetAllAsync() =>
            await _dbSet.ToListAsync();

        public async Task<T> GetByIdAsync(int id) =>
            await _dbSet.FindAsync(id);

        public async Task<GeneralResponse> AddAsync(T entity)
        {

            if (await FindDniAsync(entity))
                return new GeneralResponse(false, "El D.N.I ya existe");
            try
            {
                await _dbSet.AddAsync(entity);
                await _context.SaveChangesAsync();
                return new GeneralResponse(true, "Registro agregado con éxito");
            }
            catch (Exception ex) 
            {
                return new GeneralResponse(false,ex.Message);
            }
            

        }
        public async Task<GeneralResponse> DeleteAsync(int id)
        {
            var entity = await _dbSet.FindAsync(id);
            if (entity != null)
            {
                _dbSet.Remove(entity);
                await _context.SaveChangesAsync();
                return new GeneralResponse(true, "Registro Eliminado");
            }
            return new GeneralResponse(false, "Error al eliminar");
        }




        public async Task<GeneralResponse> UpdateAsync(T entity)
        {
            try
            {
                _dbSet.Update(entity);
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
