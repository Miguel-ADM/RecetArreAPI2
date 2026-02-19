using System.ComponentModel.DataAnnotations;

namespace RecetArreAPI2.Models
{
    public class Ingrediente
    {
        public int Id { get; set; }

        [Required]
        [StringLength(200, MinimumLength = 10)]
        public string Nombre { get; set; } = default!;

        [Required]
        [StringLength(50, MinimumLength = 10)]
        public string Unidad_medida { get; set; } = "Gr"; //Los gramos serán el valor deafult

        [Required]
        [StringLength(2000, MinimumLength = 100)]
        public string Descripcion { get; set; } = default!;

        public DateTime CreadoUtc { get; set; } = DateTime.UtcNow;
    }
}
