namespace TiendaPoniq.Domain.Entities
{
    public class ItemPedido
    {
        public int Id { get; set; }
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }

        public int PedidoId { get; set; }
        public Pedido Pedido { get; set; } = null!;

        public int VarianteProductoId { get; set; }
        public VarianteProducto VarianteProducto { get; set; } = null!;

        public decimal Subtotal => Cantidad * PrecioUnitario;
    }
}