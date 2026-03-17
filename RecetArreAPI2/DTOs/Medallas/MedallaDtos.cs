namespace RecetArreAPI2.DTOs.Medallas
{
    public class MedallaDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = default!;
        public string Descripcion { get; set; } = default!;
        public string Icono { get; set; } = default!;
        public string TipoReq { get; set; } = default!;
        public int CantidadReq { get; set; } = default!;
        public Boolean Habilitada { get; set; } = default!;
        public DateTime CreadoUtc { get; set; }

        public List<int> UsuarioIds { get; set; } = default!;
    }

    public class MedallaCreacionDto
    {
        public string Nombre { get; set; } = default!;
        public string Descripcion { get; set; } = default!;
        public string Icono { get; set; } = default!;
        public string TipoReq { get; set; } = default!;
        public int CantidadReq { get; set; } = default!;
        public Boolean Habilitada { get; set; } = default!;
    }

    public class MedallaModificacionDto
    {
        public string Nombre { get; set; } = default!;
        public string Descripcion { get; set; } = default!;
        public string Icono { get; set; } = default!;
        public string TipoReq { get; set; } = default!;
        public int CantidadReq { get; set; } = default!;
        public Boolean Habilitada { get; set; } = default!;
    }
}
