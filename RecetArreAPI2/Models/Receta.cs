using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RecetArreAPI2.Models
{
    public class Receta
    {
        public int Id { get; set; }

        [Required]
        [StringLength(200, MinimumLength = 100)]
        public string Nombre { get; set; } = string.Empty;

        [Required]
        [StringLength(10000, MinimumLength = 1000)]
        public string Instrucciones { get; set; } = string.Empty;

        //Relación con tiempos
        //[ForeignKey("Tiempo")]
        //public string? paraTiempoId { get; set; }
        //public Tiempo? paraTiempo { get; set; }

        //Relación con usuario
        [ForeignKey("ApplicationUser")]
        public string? creadoPorUsuarioId { get; set; }
        public ApplicationUser? creadoPorUsuario { get; set; }

        [Required]
        public DateTime CreadoUtc = DateTime.UtcNow;

        //Relacion con categoria
        public ICollection<Ingrediente> Ingredientes { get; set; } = new List<Ingrediente>();

        //Relación muchos a muchos con tabla intermedia
        public ICollection<Categoria> Categorias { get; set; } = new List<Categoria>();

        //TODO: navegación a comentarios
    }
}
