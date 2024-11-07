using Digitalers.DomainLayer.Entities;
using Digitalers.Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Digitalers.DataAccessLayer.Mappers
{
    public static class ProfesorEntityExtension
    {
        public static ProfesorDto ToDto(this Profesor entity)
            => new ProfesorDto()
            {
                Id = entity.Id,
                Dni = entity.Dni,
                Nombre = entity.Nombre,
                Especialidad = entity.Especialidad,
                
                //Cursos = entity.Cursos.ToDto(),
                
            };

        public static List<ProfesorDto> ToDto(this List<Profesor> entities)
            => entities.Select(ToDto).ToList();
    }
}
