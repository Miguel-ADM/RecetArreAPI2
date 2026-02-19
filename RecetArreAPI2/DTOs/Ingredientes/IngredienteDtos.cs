namespace RecetArreAPI2.DTOs.Ingredientes
{
    public class IngredienteDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = default!;

        public string Unidad_medida { get; set; } = "Gr";

        public string Descripcion { get; set; } = default!;

        public DateTime CreatedUtc { get; set; } = default!;
    }

    public class CrearIngredienteDto
    {
        public string Nombre { get; set; } = default!;
        public string Descripcion { get; set; } = "Gr";

    }

    public class ModificarIngredienteDto
    {
        public string Nombre { get; set; } = default!;
        public string Descripcion { get; set; } = "Gr";

    }

}
