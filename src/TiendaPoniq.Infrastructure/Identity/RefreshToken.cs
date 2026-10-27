namespace TiendaPoniq.Infrastructure.Identity
{
    public class RefreshToken
    {
        public int Id { get; set; }
        public string Token { get; set; } = string.Empty;
        public string ApplicationUserId { get; set; } = string.Empty;
        public ApplicationUser ApplicationUser { get; set; } = null!;
        public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;
        public DateTime FechaExpiracion { get; set; }
        public bool Revocado { get; set; } = false;
    }
}