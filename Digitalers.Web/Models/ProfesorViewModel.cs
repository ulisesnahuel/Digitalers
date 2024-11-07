using Digitalers.Shared.Dtos;
using System.ComponentModel.DataAnnotations;

namespace Digitalers.Web.Models
{
    public class ProfesorViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "El D.N.I es obligatorio")]
        [RegularExpression(@"\d{8}", ErrorMessage = "El D.N.I es incorrecto")]
        public string Dni { get; set; }
        [Required(ErrorMessage = "El Nombre es obligatorio")]

        public string Nombre { get; set; }
        [Required(ErrorMessage = "La especialidad es obligatorio")]

        public string Especialidad { get; set; }

        public List<CursoDto> Cursos { get; set; } = new List<CursoDto>();
        public string? Error { get; set; }

    }
}
