using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TiendaPoniq.Domain.Entities;
using TiendaPoniq.Infrastructure.Identity;

namespace TiendaPoniq.Infrastructure.Persistence
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Producto> Productos => Set<Producto>();
        public DbSet<Categoria> Categorias => Set<Categoria>();
        public DbSet<Marca> Marcas => Set<Marca>();
        public DbSet<VarianteProducto> VariantesProducto => Set<VarianteProducto>();
        public DbSet<Pedido> Pedidos => Set<Pedido>();
        public DbSet<ItemPedido> ItemsPedido => Set<ItemPedido>();
        public DbSet<RefreshToken> RefreshTokens => Set<RefreshToken>();
    }
}