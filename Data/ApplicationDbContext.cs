using Microsoft.EntityFrameworkCore;
using WebApiUmg.Models;

namespace WebApiUmg.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Curso> Cursos => Set<Curso>();
        public DbSet<Estudiante> Estudiantes => Set<Estudiante>();
        public DbSet<Docente> Docentes => Set<Docente>();
        public DbSet<Asignacion> Asignaciones => Set<Asignacion>();
    }
}
