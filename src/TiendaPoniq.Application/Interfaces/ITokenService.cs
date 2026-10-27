namespace TiendaPoniq.Application.Interfaces
{
    public interface ITokenService
    {
        (string Token, DateTime Expiracion) GenerarToken(string userId, string email, IList<string> roles);
        string GenerarRefreshToken();
    }
}