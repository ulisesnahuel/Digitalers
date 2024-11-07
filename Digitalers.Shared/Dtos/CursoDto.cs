using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Digitalers.Shared.Dtos
{
    public class CursoDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int Duracion { get; set; }
        public int ProfesorId { get; set; }
        public string? ProfesorNombre { get; set; }
        public List<EstudianteDto> Estudiantes { get; set; } = new List<EstudianteDto>();

    }
}
