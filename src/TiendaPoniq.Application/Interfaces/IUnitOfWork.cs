using TiendaPoniq.Domain.Entities;

namespace TiendaPoniq.Application.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<Producto, int> Productos { get; }
        IGenericRepository<Categoria, int> Categorias { get; }
        IGenericRepository<Marca, int> Marcas { get; }
        IGenericRepository<VarianteProducto, int> VariantesProducto { get; }
        IGenericRepository<Pedido, int> Pedidos { get; }
        IGenericRepository<ItemPedido, int> ItemsPedido { get; }

        Task<int> SaveChangesAsync();
    }
}