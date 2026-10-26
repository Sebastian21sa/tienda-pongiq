using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using TiendaPoniq.Application.Interfaces;

namespace TiendaPoniq.Infrastructure.Identity
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;

        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public (string Token, DateTime Expiracion) GenerarToken(string userId, string email, IList<string> roles)
        {
            var jwtSettings = _configuration.GetSection("Jwt");
            var secretKey = jwtSettings["Secret"] ?? throw new InvalidOperationException("Falta configurar Jwt:Secret");
            var issuer = jwtSettings["Issuer"];
            var audience = jwtSettings["Audience"];
            var minutosExpiracion = int.Parse(jwtSettings["ExpiracionMinutos"] ?? "60");

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, userId),
                new Claim(JwtRegisteredClaimNames.Email, email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            foreach (var rol in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, rol));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var credenciales = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var expiracion = DateTime.UtcNow.AddMinutes(minutosExpiracion);

            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: expiracion,
                signingCredentials: credenciales);

            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

            return (tokenString, expiracion);
        }
    }
}