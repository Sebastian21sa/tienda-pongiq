namespace TiendaPoniq.Application.DTOs
{
    public class ProductoDetalleDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public bool Activo { get; set; }
        public int CategoriaId { get; set; }
        public string CategoriaNombre { get; set; } = string.Empty;
        public int MarcaId { get; set; }
        public string MarcaNombre { get; set; } = string.Empty;
        public List<VarianteProductoDto> Variantes { get; set; } = new List<VarianteProductoDto>();
    }
}