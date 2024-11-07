using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Digitalers.DomainLayer.Entities
{
    public class Estudiante
    {
        public int Id { get; set; } 
        public string Nombre { get; set; } = string.Empty;
        public string Dni { get; set; } = string.Empty;
        public int Edad { get; set; }
        public List<Curso>? Cursos { get; set; }
    }
}
