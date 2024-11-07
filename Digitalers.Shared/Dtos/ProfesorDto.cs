using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Digitalers.Shared.Dtos
{
    public class ProfesorDto
    {
        public int Id { get; set; } 
        public string Dni {  get; set; }
        public string Nombre { get; set; }
        public string Especialidad { get; set; }

        public List<CursoDto> Cursos { get; set; } = new List<CursoDto>();


    }
}
