using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Digitalers.DataAccessLayer.Contracts
{
    public interface IPerson<T> where T : class
    {
        Task<T> GetByDniAsync(string dni);        
    }
}
