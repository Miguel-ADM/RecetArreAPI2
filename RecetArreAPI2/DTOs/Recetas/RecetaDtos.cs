using System.ComponentModel.DataAnnotations;

namespace RecetArreAPI2.DTOs.Recetas
{
    public class RecetaDto
    {
        public string Nombre { get; set; } = default!;
        //public string? Descripcion { get; set; } = default!;
        public string Instrucciones { get; set; } = default!;
        //public int TiempoPrparacionMinutos { get; set; } = default!;
        //public int TiempoCoccionMinutos { get; set; } = default!;
        //public int Porciones { get; set; } = default!;
        //public bool EstaPublicado { get; set; } = default!;
        public DateTime CreadoUtc { get; set; } = default!;
        public string creadoPorUsuarioId { get; set; } = default!;
        public List<int> CategoriaIds { get; set; } = default!;
        public List<int> IngredienteIds { get; set; } = default!;
    }

    public class RecetaCreacionDto
    {
        [Required]
        [StringLength(120, MinimumLength = 3)]
        public string Nombre { get; set; } = default!;

        //[StringLength(1000)]
        //public string? Descripcion { get; set; }

        [Required]
        [StringLength(15000)]
        public string Instrucciones { get; set; } = default!;

        //[Range(0, 24 * 60)]
        //public int TiempoPreparacionMinutos { get; set; }

        //[Range(0, 24 * 60)]
        //public int TiempoCoccionMinutos { get; set; }

        //[Range(1, 100)]
        //public int Porciones { get; set; } = 1;

        //public bool EstaPublicado { get; set; } = true;

        public List<int> CategoriaIds { get; set; } = new();
        public List<int> IngredienteIds { get; set; } = new();
    }

    public class RecetaModificacionDto
    {
        [Required]
        [StringLength(120, MinimumLength = 3)]
        public string Nombre { get; set; } = default!;

        //[StringLength(1000)]
        //public string? Descripcion { get; set; }

        [Required]
        [StringLength(15000)]
        public string Instrucciones { get; set; } = default!;

        //[Range(0, 24 * 60)]
        //public int TiempoPreparacionMinutos { get; set; }

        //[Range(0, 24 * 60)]
        //public int TiempoCoccionMinutos { get; set; }

        //[Range(1, 100)]
        //public int Porciones { get; set; } = 1;

        //public bool EstaPublicado { get; set; } = true;

        public List<int> CategoriaIds { get; set; } = new();
        public List<int> IngredienteIds { get; set; } = new();
    }
}
