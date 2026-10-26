namespace TiendaPoniq.Application.DTOs
{
    public class AuthResponseDto
    {
        public string Token { get; set; } = string.Empty;
        public DateTime Expiracion { get; set; }
        public string Email { get; set; } = string.Empty;
        public string Nombre { get; set; } = string.Empty;
    }
}