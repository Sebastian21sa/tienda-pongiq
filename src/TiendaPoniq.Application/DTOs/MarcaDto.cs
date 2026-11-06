namespace TiendaPoniq.Application.DTOs
{
    public class MarcaDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string? PaisOrigen { get; set; }
        public bool Activa { get; set; }
    }
}