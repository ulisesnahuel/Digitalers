using Digitalers.Shared.Dtos;

namespace Digitalers.Web.Services.Contracts
{
    public interface ICursoService
    {
        Task<GeneralResponse> AddCursoAsync(CursoDto model);
        Task<GeneralResponse> UpdateCursoAsync(CursoDto model);
        Task<GeneralResponse> DeleteCursoAsync(int id);
        Task<IEnumerable<CursoDto>> GetCursosAsync();
        Task<CursoDto> GetByIdAsync(int id);
    }
}
