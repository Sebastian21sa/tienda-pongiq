namespace TiendaPoniq.Application.DTOs
{
    public class VarianteProductoDto
    {
        public int Id { get; set; }
        public string Sku { get; set; } = string.Empty;
        public string Atributo { get; set; } = string.Empty;
        public decimal Precio { get; set; }
        public int Stock { get; set; }
    }
}