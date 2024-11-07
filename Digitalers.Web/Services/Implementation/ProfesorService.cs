using Digitalers.Shared.Dtos;
using Digitalers.Web.Services.Contracts;

namespace Digitalers.Web.Services.Implementation
{
    public class ProfesorService: IProfesorService
    {

        private readonly HttpClient _http;

        public ProfesorService(HttpClient httpClient)
        {
            _http = httpClient;
            _http.BaseAddress = new Uri("http://localhost:5104/api/"); 
        }

        public async Task<GeneralResponse> AddProfesorAsync(ProfesorDto model)
        {
            var response = await _http.PostAsJsonAsync("Profesor", model);
            return await response.Content.ReadFromJsonAsync<GeneralResponse>();
            
            
        }

        public async Task<GeneralResponse> UpdateProfesorAsync(ProfesorDto model)
        {
            var response = await _http.PutAsJsonAsync("Profesor", model);
            
            return await response.Content.ReadFromJsonAsync<GeneralResponse>();
           
        }

        public async Task<GeneralResponse> DeleteProfesorAsync(int id)
        {
            var response = await _http.DeleteAsync($"Profesor?id={id}");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<GeneralResponse>();
            }
            return new GeneralResponse(false, "Error al eliminar profesor");
        }

        public async Task<List<ProfesorDto>> GetProfesoresAsync()
        {
            var response = await _http.GetAsync("Profesor");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<List<ProfesorDto>>();
            }
            return new List<ProfesorDto>();
        }

        public async Task<ProfesorDto?> GetByIdAsync(int id)
        {
            var response = await _http.GetAsync($"Profesor/{id}");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<ProfesorDto>();
            }
            return null;
        }
        public async Task<ProfesorDto?> GetByDniAsync(string dni)
        {
            var response = await _http.GetAsync($"Profesor/dni/{dni}");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<ProfesorDto>();
            }
            return null;
        }

    }
}
