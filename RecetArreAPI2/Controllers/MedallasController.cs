using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RecetArreAPI2.Context;
using RecetArreAPI2.Models;
using RecetArreAPI2.DTOs.Medallas;

namespace RecetArreAPI2.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MedallasController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;
        private readonly UserManager<ApplicationUser> userManager;

        public MedallasController(
            ApplicationDbContext context,
            IMapper mapper,
            UserManager<ApplicationUser> userManager
        )
        {
            this.context = context;
            this.mapper = mapper;
            this.userManager = userManager;
        }

        // GET: api/medallas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MedallaDto>>> GetMedallas()
        {
            var medallas = await context.Medallas
                .OrderByDescending(c => c.CreadoUtc)
                .ToListAsync();

            return Ok(mapper.Map<List<MedallaDto>>(medallas));
        }

        // GET: api/medallas/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<MedallaDto>> GetMedalla(int id)
        {
            var medalla = await context.Medallas.FirstOrDefaultAsync(c => c.Id == id);

            if (medalla == null)
            {
                return NotFound(new { mensaje = "Categoría no encontrada" });
            }

            return Ok(mapper.Map<MedallaDto>(medalla));
        }

        // POST: api/medallas
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<MedallaDto>> CreateMedallas(MedallaCreacionDto medallaCreacionDto)
        {
            // Validar que el nombre no esté duplicado
            var existe = await context.Medallas
                .AnyAsync(c => c.nombre.ToLower() == medallaCreacionDto.Nombre.ToLower());

            if (existe)
            {
                return BadRequest(new { mensaje = "Ya existe una medalla con ese nombre" });
            }

            // Obtener el usuario autenticado
            var usuarioId = userManager.GetUserId(User);
            if (string.IsNullOrEmpty(usuarioId))
            {
                return Unauthorized(new { mensaje = "Usuario no autenticado" });
            }

            var medalla = mapper.Map<Medalla>(medallaCreacionDto);
            medalla.CreadoUtc = DateTime.UtcNow;

            context.Medallas.Add(medalla);
            await context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetMedalla), new { id = medalla.Id }, mapper.Map<MedallaDto>(medalla));
        }

        // PUT: api/medallas/{id}
        [HttpPut("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> UpdateMedalla(int id, MedallaModificacionDto medallaModificacionDto)
        {
            var medalla = await context.Medallas.FirstOrDefaultAsync(c => c.Id == id);

            if (medalla == null)
            {
                return NotFound(new { mensaje = "Medalla no encontrada" });
            }

            // Validar que el nombre no esté duplicado (si cambió)
            if (!medalla.nombre.Equals(medallaModificacionDto.Nombre, StringComparison.OrdinalIgnoreCase))
            {
                var existe = await context.Medallas
                    .AnyAsync(c => c.nombre.ToLower() == medallaModificacionDto.Nombre.ToLower() && c.Id != id);

                if (existe)
                {
                    return BadRequest(new { mensaje = "Ya existe una medalla con ese nombre" });
                }
            }

            mapper.Map(medallaModificacionDto, medalla);
            context.Medallas.Update(medalla);
            await context.SaveChangesAsync();

            return Ok(new { mensaje = "Medalla actualizada exitosamente", data = mapper.Map<MedallaDto>(medalla) });
        }

        // DELETE: api/medallas/{id}
        [HttpDelete("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> DeleteMedalla(int id)
        {
            var medalla = await context.Medallas.FirstOrDefaultAsync(c => c.Id == id);

            if (medalla == null)
            {
                return NotFound(new { mensaje = "Medalla no encontrada" });
            }

            var medallaModificada = mapper.Map<Medalla>(medalla);
            medallaModificada.habilitada = false;

            mapper.Map(medallaModificada, medalla);
            //context.Medallas.Remove(medalla);
            context.Medallas.Update(medalla);
            await context.SaveChangesAsync();

            return Ok(new { mensaje = "Medalla deshabilitada exitosamente" });
        }
    }
}
