using System.ComponentModel.DataAnnotations;

namespace RecetArreAPI2.Models
{
    public class Medalla
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 20)]
        public String nombre { get; set; } = default!;

        [Required]
        [StringLength(500, MinimumLength = 50)]
        public String descripcion { get; set; } = default!;

        [Required]
        [StringLength(500, MinimumLength = 10)]
        public String icono { get; set; } = default!;

        [Required]
        [StringLength(200, MinimumLength = 20)]
        public String tipoReq { get; set; }  = default!;

        [Required]
        public int cantidadReq { get; set; }

        [Required]
        public Boolean habilitada { get; set; }

        [Required]
        public DateTime CreadoUtc { get; set; } = DateTime.UtcNow;

        //navigation
        public ICollection<ApplicationUser> Usuarios { get; set; } = new List<ApplicationUser>();
    }
}
