namespace TiendaPoniq.Application.DTOs
{
    public class ProductoCreateDto
    {
        public string Nombre { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public int CategoriaId { get; set; }
        public int MarcaId { get; set; }
    }
}