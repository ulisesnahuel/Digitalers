using Microsoft.AspNetCore.Mvc.Rendering;

namespace Digitalers.Web.Models
{
    public class EditCursoViewModel
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int Duracion { get; set; }
        public int ProfesorId { get; set; }

        public List<ProfesorViewModel>? Profesores { get; set; }

        public List<int> EstudiantesSeleccionados { get; set; }

        public List<EstudianteViewModel>? EstudiantesDisponibles { get; set; }
    }
}
