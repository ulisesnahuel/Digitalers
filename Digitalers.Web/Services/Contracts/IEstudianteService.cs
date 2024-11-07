using Digitalers.Shared.Dtos;

namespace Digitalers.Web.Services.Contracts
{
    public interface IEstudianteService
    {
        Task<GeneralResponse> AddEstudianteAsync(EstudianteDto model);
        Task<GeneralResponse> UpdateEstudianteAsync(EstudianteDto model);
        Task<GeneralResponse> DeleteEstudianteAsync(int id);
        Task<List<EstudianteDto>> GetEstudiantesAsync();
        Task<EstudianteDto?> GetByIdAsync(int id);
        Task<EstudianteDto?> GetByDniAsync(string dni);

    }
}
