namespace TiendaPoniq.Domain.Entities
{
    public class Producto
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public bool Activo { get; set; } = true;
        public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;

        public int CategoriaId { get; set; }
        public Categoria Categoria { get; set; } = null!;

        public int MarcaId { get; set; }
        public Marca Marca { get; set; } = null!;

        public ICollection<VarianteProducto> Variantes { get; set; } = new List<VarianteProducto>();
    }
}