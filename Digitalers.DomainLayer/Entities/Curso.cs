using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Digitalers.DomainLayer.Entities
{
    public class Curso
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int Duracion { get; set; }
        public Profesor? Profesor { get; set; }

        public int ProfesorId {  get; set; }   

        public List<Estudiante>? Estudiantes { get; set; }  = new List<Estudiante>();

    }
}
