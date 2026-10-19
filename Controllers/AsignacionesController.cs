using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiUmg.Data;
using WebApiUmg.Models;

namespace WebApiUmg.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AsignacionesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public AsignacionesController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Asignacion>>> GetAsignaciones()
        {
            return await _context.Asignaciones
                .Include(a => a.Estudiante)
                .Include(a => a.Curso)
                .Include(a => a.Docente)
                .ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Asignacion>> GetAsignacion(int id)
        {
            var asignacion = await _context.Asignaciones
                .Include(a => a.Estudiante)
                .Include(a => a.Curso)
                .Include(a => a.Docente)
                .FirstOrDefaultAsync(a => a.AsignacionId == id);

            if (asignacion == null) return NotFound();
            return asignacion;
        }

        [HttpPost]
        public async Task<ActionResult<Asignacion>> PostAsignacion(Asignacion asignacion)
        {
            if (!await _context.Estudiantes.AnyAsync(e => e.EstudianteId == asignacion.EstudianteId) ||
                !await _context.Cursos.AnyAsync(c => c.CursoId == asignacion.CursoId) ||
                !await _context.Docentes.AnyAsync(d => d.DocenteId == asignacion.DocenteId))
            {
                return BadRequest("Uno o más identificadores no existen en el sistema.");
            }

            _context.Asignaciones.Add(asignacion);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetAsignacion), new { id = asignacion.AsignacionId }, asignacion);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsignacion(int id, Asignacion asignacion)
        {
            if (id != asignacion.AsignacionId) return BadRequest();

            if (!await _context.Estudiantes.AnyAsync(e => e.EstudianteId == asignacion.EstudianteId) ||
                !await _context.Cursos.AnyAsync(c => c.CursoId == asignacion.CursoId) ||
                !await _context.Docentes.AnyAsync(d => d.DocenteId == asignacion.DocenteId))
            {
                return BadRequest("Uno o más identificadores no existen en el sistema.");
            }

            _context.Entry(asignacion).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Asignaciones.Any(e => e.AsignacionId == id)) return NotFound();
                throw;
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsignacion(int id)
        {
            var asignacion = await _context.Asignaciones.FindAsync(id);
            if (asignacion == null) return NotFound();

            _context.Asignaciones.Remove(asignacion);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
