using Digitalers.Shared.Dtos;

namespace Digitalers.Web.Services.Contracts
{
    public interface IProfesorService
    {
        Task<GeneralResponse> AddProfesorAsync(ProfesorDto model);
         Task<GeneralResponse> UpdateProfesorAsync(ProfesorDto model);
         Task<GeneralResponse> DeleteProfesorAsync(int id);
         Task<List<ProfesorDto>> GetProfesoresAsync();
         Task<ProfesorDto?> GetByIdAsync(int id);
         Task<ProfesorDto?> GetByDniAsync(string dni);





    }
}
