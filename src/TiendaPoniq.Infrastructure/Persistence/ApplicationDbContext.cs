using Microsoft.EntityFrameworkCore;
using TiendaPoniq.Domain.Entities;

namespace TiendaPoniq.Infrastructure.Persistence
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Producto> Productos => Set<Producto>();
        public DbSet<Categoria> Categorias => Set<Categoria>();
        public DbSet<Marca> Marcas => Set<Marca>();
        public DbSet<VarianteProducto> VariantesProducto => Set<VarianteProducto>();
        public DbSet<Usuario> Usuarios => Set<Usuario>();
        public DbSet<Pedido> Pedidos => Set<Pedido>();
        public DbSet<ItemPedido> ItemsPedido => Set<ItemPedido>();
    }
}