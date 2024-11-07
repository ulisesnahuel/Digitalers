using Digitalers.DomainLayer.Entities;
using Digitalers.Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Digitalers.DataAccessLayer.Mappers
{
    public static class CursoEntityExtension
    {
        public static CursoDto ToDto(this Curso entity)
           => new CursoDto()
           {
               Id = entity.Id,
               Nombre = entity.Nombre,
               Duracion = entity.Duracion,
               ProfesorId = entity.ProfesorId,        
               ProfesorNombre = entity.Profesor.Nombre,
               Estudiantes = entity.Estudiantes.ToDto()
           };
        public static List<CursoDto> ToDto(this List<Curso> entities)
            => entities.Select(ToDto).ToList();
    }
}
