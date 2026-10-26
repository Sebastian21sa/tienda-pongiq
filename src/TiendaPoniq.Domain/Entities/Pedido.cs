using TiendaPoniq.Domain.Enums;

namespace TiendaPoniq.Domain.Entities
{
    public class Pedido
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; } = DateTime.UtcNow;
        public EstadoPedido Estado { get; set; } = EstadoPedido.Pendiente;
        public decimal Total { get; set; }

        public string UsuarioId { get; set; } = string.Empty;

        public ICollection<ItemPedido> Items { get; set; } = new List<ItemPedido>();
    }
}