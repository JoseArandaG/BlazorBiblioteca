namespace BlazorBiblioteca.Shared
{
    public class Libro
    {
        public int Id { get; set; }
        public string NombreLibro { get; set; } = String.Empty;
        public string Autor { get; set; } = String.Empty;
        public int NumPaginas { get; set; }
        public DateOnly FechaPublicacion { get; set; }
    }
}
