namespace TiendaPoniq.Domain.Entities
{
    public class VarianteProducto
    {
        public int Id { get; set; }
        public string Sku { get; set; } = string.Empty;
        public string Atributo { get; set; } = string.Empty;
        public decimal Precio { get; set; }
        public int Stock { get; set; }

        public int ProductoId { get; set; }
        public Producto Producto { get; set; } = null!;

        public ICollection<ItemPedido> ItemsPedido { get; set; } = new List<ItemPedido>();
    }
}