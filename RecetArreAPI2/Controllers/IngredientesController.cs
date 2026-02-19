using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RecetArreAPI2.Context;
using RecetArreAPI2.DTOs.Ingredientes;
using RecetArreAPI2.Models;

namespace RecetArreAPI2.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class IngredientesController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;
        private readonly UserManager<ApplicationUser> userManager;

        public IngredientesController(ApplicationDbContext context, IMapper mapper, UserManager<ApplicationUser> userManager)
        {
            this.context = context;
            this.mapper = mapper;
            this.userManager = userManager;
        }

        //Get todas las recetas
        //Get api/Ingredientes
       [HttpGet]
        public async Task<ActionResult<IEnumerable<IngredienteDto>>> GetIngredientes()
        {
            var ingredientes = await context.Ingredientes
                .OrderByDescending(c => c.CreadoUtc)
                .ToListAsync();

            return Ok(mapper.Map<List<IngredienteDto>>(ingredientes));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IngredienteDto>> GetIngrediente(int id)
        {
            var ingrediente = await context.Ingredientes.FirstOrDefaultAsync(c => c.Id == id);

            if (ingrediente == null)
            {
                return NotFound(new { mensaje = "Ingrediente no encontrado" });
            }

            return Ok(mapper.Map<IngredienteDto>(ingrediente));
        }

        [HttpPost]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<IngredienteDto>> CreateIngrediente(CrearIngredienteDto crearIngredienteDto)
        {
            var exist = await context.Ingredientes
                .AnyAsync( c => c.Nombre.ToLower() == crearIngredienteDto.Nombre.ToLower() );

            //Revisar si ese nombre ya existe
            if (exist)
            {
                return BadRequest(new { mensaje = "Ya existe una categoría con ese nombre"});
            }

            //obtener al usuario autentificado
            //var usuarioId = userManager.GetUserId(User);
            //if (string.IsNullOrEmpty(usuarioId))
            //{
            //    return Unauthorized(new { menaje = "Usuario no autenticado" });
            //}

            var ingrediendte = mapper.Map<Ingrediente>(crearIngredienteDto);
            ingrediendte.CreadoUtc = DateTime.UtcNow;

            context.Ingredientes.Add(ingrediendte);
            await context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetIngrediente), new { id = ingrediendte.Id }, mapper.Map<Ingrediente>(crearIngredienteDto));
        }

        [HttpPut("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> UpadteIngrediente(int id, ModificarIngredienteDto modificarIngredienteDto)
        {
            var ingrediente = await context.Ingredientes.FirstOrDefaultAsync(c => c.Id == id);

            if(ingrediente == null)
            {
                return NotFound(new { mensaje = "Ingrediente no encontrado" });
            }

            //validar que el nombre no esté duplicado
            if(!ingrediente.Nombre.Equals(modificarIngredienteDto.Nombre, StringComparison.OrdinalIgnoreCase))
            {
                var existe = await context.Ingredientes
                    .AnyAsync(c => c.Nombre.ToLower() == modificarIngredienteDto.Nombre.ToLower() && c.Id != id);


                if (existe)
                {
                    return BadRequest(new { mensaje = "Ya existe un ingrediente con ese nombre"});
                }
            }

            mapper.Map(modificarIngredienteDto, ingrediente);
            context.Ingredientes.Update(ingrediente);
            await context.SaveChangesAsync();

            return Ok(new { mensaje = "Categoría modificada exitosamente", data = mapper.Map<Ingrediente>(ingrediente)});
        }

        [HttpDelete("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> DeleteIngrediente(int id)
        {
            var ingrediente = await context.Ingredientes.FirstOrDefaultAsync(c => c.Id == id);

            if(ingrediente == null)
            {
                return NotFound(new { mensaje = "ingrediente no encontrado" });
            }

            context.Ingredientes.Remove(ingrediente);
            await context.SaveChangesAsync();

            return Ok(new { mensaje = "Categoria eliminada exitosamente" });
        }
    }
}
