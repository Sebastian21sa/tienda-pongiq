using Microsoft.AspNetCore.Identity;

namespace TiendaPoniq.Infrastructure.Identity
{
    public class ApplicationUser : IdentityUser
    {
        public string Nombre { get; set; } = string.Empty;
        public string Apellido { get; set; } = string.Empty;
        public DateTime FechaRegistro { get; set; } = DateTime.UtcNow;
    }
}