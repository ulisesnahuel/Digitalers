using Digitalers.Shared.Dtos;
using Digitalers.Web.Services.Contracts;
using Microsoft.Extensions.Options;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Digitalers.Web.Services.Implementation
{
    public class CursoService : ICursoService
    {
        private readonly HttpClient _http;

        public CursoService(HttpClient httpClient)
        {
            _http = httpClient;
            _http.BaseAddress = new Uri("http://localhost:5104/api/");
        }
        public async Task<GeneralResponse> AddCursoAsync(CursoDto model)
        {
            try
            {
                var response = await _http.PostAsJsonAsync("Curso", model);
                response.EnsureSuccessStatusCode();
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                };
                if (response.IsSuccessStatusCode)
                {
                    string responseBody = await response.Content.ReadAsStringAsync();
                    GeneralResponse result = JsonSerializer.Deserialize<GeneralResponse>(responseBody, options);
                    return result ?? new GeneralResponse(false, "error al deserealizar");
                }
                else
                {
                    string responseBody = await response.Content.ReadAsStringAsync();
                    GeneralResponse result = JsonSerializer.Deserialize<GeneralResponse>(responseBody, options);
                    return result;
                }
            }
            catch (HttpRequestException ex)
            {
                return new GeneralResponse(false, $"Error de red al agregar curso:{ex.Message}");
            }

        }

        public async Task<GeneralResponse> UpdateCursoAsync(CursoDto model)
        {
            var result = await _http.PutAsJsonAsync("Curso", model);
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            if (result.IsSuccessStatusCode)
            {
                string responseBody = await result.Content.ReadAsStringAsync();
                GeneralResponse response = JsonSerializer.Deserialize<GeneralResponse>(responseBody, options);
                return response;

            }
            else
            {
                return new GeneralResponse(false, $"La solicitud para actualizar el curso falló. Código de estado: {result.StatusCode}");
            }
        }

        public async Task<GeneralResponse> DeleteCursoAsync(int id)
        {
            var result = await _http.DeleteAsync($"Curso?id={id}");
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            if (result.IsSuccessStatusCode)
            {
                string responseBody = await result.Content.ReadAsStringAsync();
                GeneralResponse response = JsonSerializer.Deserialize<GeneralResponse>(responseBody, options);
                return response;

            }
            else
            {
                return new GeneralResponse(false, $"La solicitud para eliminar el titular falló. Código de estado: {result.StatusCode}");
            }
        }
        public async Task<IEnumerable<CursoDto>> GetCursosAsync()
        {
            var response = await _http.GetAsync("Curso");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<List<CursoDto>>();
            }
            else
            {
                return new List<CursoDto>();
            }

        }

        public async Task<CursoDto> GetByIdAsync(int id)
        {
            try
            {
                var response = await _http.GetAsync($"Curso/{id}");
                if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    return null;
                }
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<CursoDto>();
                }
                else
                {
                    return null!;
                }
            }
            catch (Exception ex)
            {
                {
                    throw new Exception($"Request failed: {ex.Message}");
                }

            }
        }


    }
}
