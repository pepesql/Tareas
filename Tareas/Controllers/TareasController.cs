using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tareas.Data;
using Tareas.DTOS;
using Tareas.Models;

namespace Tareas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TareasController : ControllerBase
    {

        private readonly AppDbContext _context;

        public TareasController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/tareas
        [HttpGet("GetTareas")]
        public async Task<ActionResult<IEnumerable<Tarea>>> GetTareas()
        {
            return await _context.Tareas.ToListAsync();
        }


        [HttpGet("GetTareasPaginado")]
        public async Task<ActionResult<PagedResult<Tarea>>> GetTareasPaginado(
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 10,
            [FromQuery] EstadoTarea? estado = null,
            [FromQuery] string? responsable = null,
            [FromQuery] string? search = null)
        {
            // 1. Validar y acotar parámetros
            if (page < 1) page = 1;
            pageSize = Math.Clamp(pageSize, 1, 100);

            // 2. Construir la consulta base (aún no se ejecuta en BD)
            var query = _context.Tareas.AsNoTracking().AsQueryable();

            // 3. Filtros opcionales
            if (estado.HasValue)
                query = query.Where(t => t.Estado == estado.Value);

            if (!string.IsNullOrWhiteSpace(responsable))
                query = query.Where(t => t.Responsable.Contains(responsable));

            if (!string.IsNullOrWhiteSpace(search))
                query = query.Where(t => t.Titulo.Contains(search)
                                      || (t.Descripcion != null && t.Descripcion.Contains(search)));

            // 4. Contar el total ANTES de paginar
            var totalCount = await query.CountAsync();

            // 5. Ordenar (obligatorio) y paginar
            var items = await query
                .OrderBy(t => t.FechaLimite)
                .ThenBy(t => t.Id)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            // 6. Devolver resultado paginado
            return Ok(new PagedResult<Tarea>
            {
                Items = items,
                TotalCount = totalCount,
                Page = page,
                PageSize = pageSize
            });
        }







        // GET: api/tareas/5
        [HttpGet("GetTarea")]
        public async Task<ActionResult<Tarea>> GetTarea(int id)
        {
            var tarea = await _context.Tareas.FindAsync(id);
            if (tarea == null) return NotFound();
            return tarea;
        }

        // POST: api/tareas
        [HttpPost("CreateTarea")]
        public async Task<ActionResult<Tarea>> CreateTarea(Tarea tarea)
        {
            // Validación adicional: la fecha límite no puede ser en el pasado
            if (tarea.FechaLimite < DateTime.Today)
            {
                ModelState.AddModelError(nameof(tarea.FechaLimite), "La fecha límite no puede ser anterior a hoy.");
                return ValidationProblem(ModelState);
            }

            _context.Tareas.Add(tarea);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetTarea), new { id = tarea.Id }, tarea);
        }

        // PUT: api/tareas/5
        [HttpPut("UpdateTarea")]
        public async Task<IActionResult> UpdateTarea(int id, Tarea tarea)
        {
            if (id != tarea.Id) return BadRequest();

            if (tarea.FechaLimite < DateTime.Today)
            {
                ModelState.AddModelError(nameof(tarea.FechaLimite), "La fecha límite no puede ser anterior a hoy.");
                return ValidationProblem(ModelState);
            }

            _context.Entry(tarea).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TareaExists(id)) return NotFound();
                throw;
            }
            return NoContent();
        }

        // DELETE: api/tareas/5
        [HttpDelete("DeleteTarea")]
        public async Task<IActionResult> DeleteTarea(int id)
        {
            var tarea = await _context.Tareas.FindAsync(id);
            if (tarea == null) return NotFound();

            _context.Tareas.Remove(tarea);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        private bool TareaExists(int id)
        {
            return _context.Tareas.Any(e => e.Id == id);
        }





    }
}
