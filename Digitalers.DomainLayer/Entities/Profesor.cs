using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Digitalers.DomainLayer.Entities
{
    public class Profesor
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Dni {  get; set; }
        public string Especialidad { get; set; }    

        public List<Curso>? Cursos { get; set; }


    }
}
