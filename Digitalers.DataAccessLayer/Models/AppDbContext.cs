using Digitalers.DomainLayer.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Digitalers.DataAccessLayer.Models
{
    public class AppDbContext: DbContext
    {
        public AppDbContext ( DbContextOptions<AppDbContext> options ) : base( options ) { }


        public DbSet<Curso> Cursos { get; set; }
        public DbSet<Profesor> Profesores { get; set; }
        public DbSet<Estudiante> Estudiantes {  get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Curso>()
                .HasOne(c => c.Profesor)
                .WithMany(c => c.Cursos)
                .HasForeignKey(c => c.ProfesorId)
                .OnDelete(deleteBehavior: DeleteBehavior.Cascade);

            modelBuilder.Entity<Curso>()
                .HasMany(c => c.Estudiantes)
                .WithMany(e => e.Cursos)
                .UsingEntity(j => j.ToTable("CursosEstudiantes")); // tabla intermediaria m a m

        }



    }
}
