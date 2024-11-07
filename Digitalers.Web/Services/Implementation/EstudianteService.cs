using Digitalers.Shared.Dtos;
using Digitalers.Web.Services.Contracts;
using System.Text.Json;

namespace Digitalers.Web.Services.Implementation
{
    public class EstudianteService : IEstudianteService
    {
        private readonly HttpClient _http;

        public EstudianteService(HttpClient httpClient)
        {
            _http = httpClient;
            _http.BaseAddress = new Uri("http://localhost:5104/api/"); 
        }

        public async Task<GeneralResponse> AddEstudianteAsync(EstudianteDto model)
        {
            var response = await _http.PostAsJsonAsync("Estudiante", model);

            return await response.Content.ReadFromJsonAsync<GeneralResponse>();
          
        }

        public async Task<GeneralResponse> UpdateEstudianteAsync(EstudianteDto model)
        {
            var response = await _http.PutAsJsonAsync("Estudiante", model);
            
            return await response.Content.ReadFromJsonAsync<GeneralResponse>();
           
        }

        public async Task<GeneralResponse> DeleteEstudianteAsync(int id)
        {
            var response = await _http.DeleteAsync($"Estudiante?id={id}");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<GeneralResponse>();
            }
            return new GeneralResponse(false, "Error al eliminar estudiante");
        }

        public async Task<List<EstudianteDto>> GetEstudiantesAsync()
        {


            try
            {
                var response = await _http.GetAsync("Estudiante");
                if (response.IsSuccessStatusCode)
                {
                    var estudiantes = await response.Content.ReadFromJsonAsync<List<EstudianteDto>>(new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true 
                    });
                    return estudiantes ?? new List<EstudianteDto>(); 
                }
                else
                {
                    var errorBody = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Error en la solicitud: {response.StatusCode}, Detalle: {errorBody}");
                    return new List<EstudianteDto>(); 
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Excepción al obtener estudiantes: {ex.Message}");
                return new List<EstudianteDto>(); 
            }
        }

        public async Task<EstudianteDto?> GetByIdAsync(int id)
        {
            var response = await _http.GetAsync($"Estudiante/{id}");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<EstudianteDto>();
            }
            return null;
        }


        public async Task<EstudianteDto?> GetByDniAsync(string dni)
        {
            var response = await _http.GetAsync($"Estudiante/dni/{dni}");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<EstudianteDto>();
            }
            return null;
        }
    }
}
