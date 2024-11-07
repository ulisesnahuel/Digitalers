using Digitalers.DomainLayer.Entities;
using Digitalers.Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Digitalers.DataAccessLayer.Mappers
{
    public static class EstudianteEntityExtension
    {
        public static EstudianteDto ToDto(this Estudiante entity)
            => new EstudianteDto()
            {
                Id = entity.Id,
                Dni = entity.Dni,
                Nombre = entity.Nombre,
                Edad = entity.Edad,
                //Cursos = entity.Cursos.ToDto(),

            };

        public static List<EstudianteDto> ToDto(this List<Estudiante> entities)
            => entities.Select(ToDto).ToList();
    }
}
