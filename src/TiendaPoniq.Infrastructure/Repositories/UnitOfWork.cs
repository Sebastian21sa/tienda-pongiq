using TiendaPoniq.Application.Interfaces;
using TiendaPoniq.Domain.Entities;
using TiendaPoniq.Infrastructure.Persistence;

namespace TiendaPoniq.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        private IGenericRepository<Producto, int>? _productos;
        private IGenericRepository<Categoria, int>? _categorias;
        private IGenericRepository<Marca, int>? _marcas;
        private IGenericRepository<VarianteProducto, int>? _variantesProducto;
        private IGenericRepository<Pedido, int>? _pedidos;
        private IGenericRepository<ItemPedido, int>? _itemsPedido;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }

        public IGenericRepository<Producto, int> Productos =>
            _productos ??= new GenericRepository<Producto, int>(_context);

        public IGenericRepository<Categoria, int> Categorias =>
            _categorias ??= new GenericRepository<Categoria, int>(_context);

        public IGenericRepository<Marca, int> Marcas =>
            _marcas ??= new GenericRepository<Marca, int>(_context);

        public IGenericRepository<VarianteProducto, int> VariantesProducto =>
            _variantesProducto ??= new GenericRepository<VarianteProducto, int>(_context);

        public IGenericRepository<Pedido, int> Pedidos =>
            _pedidos ??= new GenericRepository<Pedido, int>(_context);

        public IGenericRepository<ItemPedido, int> ItemsPedido =>
            _itemsPedido ??= new GenericRepository<ItemPedido, int>(_context);

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}