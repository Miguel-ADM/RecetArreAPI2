using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RecetArreAPI2.Context;
using RecetArreAPI2.DTOs.Recetas;
using RecetArreAPI2.Models;

namespace RecetArreAPI2.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RecetasController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;
        private readonly UserManager<ApplicationUser> userManager;

        public RecetasController(ApplicationDbContext context, IMapper mapper, UserManager<ApplicationUser> userManager)
        {
            this.context = context;
            this.mapper = mapper;
            this.userManager = userManager;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<RecetaDto>>> GetRecetas()
        {
            var recetas = await context.Recetas
                .Include(r => r.Categorias)
                .Include(r => r.Instrucciones)
                .ToListAsync();

            return Ok(mapper.Map<List<RecetaDto>> (recetas));
        }

        //filtrar por categorias
        [HttpGet("filtrar/categorias")]
        public async Task<ActionResult<IEnumerable<RecetaDto>>> FiltrarPorCategoria([FromQuery] List<int> categorias)
        {
            //si no hay categorias
            if (!categorias.Any()) return new List<RecetaDto>();

            var ids = categorias.Distinct().ToList();

            //devolver recetas
            var recetas = await context.Recetas
                .Include(r => r.Categorias)
                .Include (r => r.Instrucciones)
                .Where(r => r.Categorias.Any(c => ids.Contains(c.Id)))
                .OrderByDescending(r => r.CreadoUtc)
                .ToListAsync();

            return Ok(mapper.Map<List<RecetaDto>>(recetas));
        }



    }
}
