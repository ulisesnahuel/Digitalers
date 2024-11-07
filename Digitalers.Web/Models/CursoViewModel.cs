using Digitalers.Shared.Dtos;
using System.ComponentModel.DataAnnotations;

namespace Digitalers.Web.Models
{
    public class CursoViewModel
    {       
        public int Id { get; set; }
        [Required(ErrorMessage = "El Nombre es obligatorio")]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "la duracion del curso es obligatorio")]
        public int Duracion { get; set; }

        public string? ProfesorNombre { get; set; }
        [Required(ErrorMessage = "El Profesor es obligatorio")]

        public int ProfesorId { get; set; }
        public List<EstudianteDto> Estudiantes { get; set; } = new List<EstudianteDto>();
    }
}
