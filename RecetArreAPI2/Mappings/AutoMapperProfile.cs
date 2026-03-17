using AutoMapper;
using RecetArreAPI2.DTOs;
using RecetArreAPI2.DTOs.Categorias;
using RecetArreAPI2.DTOs.Ingredientes;
using RecetArreAPI2.DTOs.Recetas;
using RecetArreAPI2.Models;
using RecetArreAPI2.DTOs.Medallas;

namespace RecetArreAPI2.Mappings
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            // ApplicationUser <-> ApplicationUserDto
            CreateMap<ApplicationUser, ApplicationUserDtos>().ReverseMap();

            // Categoria mappings
            CreateMap<Categoria, CategoriaDto>();
            CreateMap<CategoriaCreacionDto, Categoria>();
            CreateMap<CategoriaModificacionDto, Categoria>();

            //Ingredientes mappings
            CreateMap<Ingrediente, IngredienteDto>();
            CreateMap<CrearIngredienteDto, Ingrediente>();
            CreateMap<ModificarIngredienteDto, Ingrediente>();

            //Receta mapping
            CreateMap<Receta, RecetaDto>()
                .ForMember(dest => dest.CategoriaIds, opt => opt.MapFrom(src => src.Categorias.Select(c => c.Id)))
                .ForMember(dest => dest.IngredienteIds, opt => opt.MapFrom(src => src.Ingredientes.Select(i => i.Id)));
            CreateMap<RecetaCreacionDto, Receta>();
            CreateMap<RecetaModificacionDto, Receta>();

            //Medalla mappings
            CreateMap<Medalla, MedallaDto>()
                .ForMember(dest => dest.UsuarioIds, opt => opt.MapFrom(src => src.Usuarios.Select(u => u.Id)));
            CreateMap<MedallaCreacionDto, Medalla>();
            CreateMap<MedallaModificacionDto, Medalla>();
            CreateMap<Medalla, Medalla>();
        }
    }
}
