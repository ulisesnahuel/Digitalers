using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Digitalers.Shared.Dtos;

namespace Digitalers.DataAccessLayer.Contracts
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);

        Task<GeneralResponse> AddAsync(T entity);
        Task<GeneralResponse> UpdateAsync(T entity);
        Task<GeneralResponse> DeleteAsync(int id);

    }
}
