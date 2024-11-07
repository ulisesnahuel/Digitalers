using Digitalers.Shared.Dtos;
using System.ComponentModel.DataAnnotations;

namespace Digitalers.Web.Models
{
    public class EstudianteViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "El Nombre es obligatorio")]
        public string Nombre { get; set; } = string.Empty;
        [Required(ErrorMessage = "El D.N.I es obligatorio")]
        [RegularExpression(@"\d{8}", ErrorMessage = "El D.N.I es incorrecto")]
        public string Dni { get; set; } = string.Empty;
        [Required(ErrorMessage = "La edad es obligatorio")]
        public int Edad { get; set; }

        public List<CursoDto> Cursos { get; set; } = new List<CursoDto>();

        public string? Error { get; set; }

    }
}
