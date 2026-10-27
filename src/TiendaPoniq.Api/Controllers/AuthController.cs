using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TiendaPoniq.Application.DTOs;
using TiendaPoniq.Application.Interfaces;
using TiendaPoniq.Infrastructure.Identity;
using TiendaPoniq.Infrastructure.Persistence;

namespace TiendaPoniq.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ITokenService _tokenService;
        private readonly ApplicationDbContext _context;

        private const int DiasExpiracionRefreshToken = 7;

        public AuthController(UserManager<ApplicationUser> userManager, ITokenService tokenService, ApplicationDbContext context)
        {
            _userManager = userManager;
            _tokenService = tokenService;
            _context = context;
        }

        [HttpPost("register")]
        public async Task<ActionResult<AuthResponseDto>> Register(RegisterDto dto)
        {
            var usuarioExistente = await _userManager.FindByEmailAsync(dto.Email);
            if (usuarioExistente != null)
            {
                return BadRequest("Ya existe un usuario registrado con ese correo.");
            }

            var nuevoUsuario = new ApplicationUser
            {
                UserName = dto.Email,
                Email = dto.Email,
                Nombre = dto.Nombre,
                Apellido = dto.Apellido,
                FechaRegistro = DateTime.UtcNow
            };

            var resultado = await _userManager.CreateAsync(nuevoUsuario, dto.Password);

            if (!resultado.Succeeded)
            {
                var errores = resultado.Errors.Select(e => e.Description);
                return BadRequest(errores);
            }

            await _userManager.AddToRoleAsync(nuevoUsuario, "Cliente");

            var respuesta = await GenerarRespuestaAutenticacionAsync(nuevoUsuario);
            return Ok(respuesta);
        }

        [HttpPost("login")]
        public async Task<ActionResult<AuthResponseDto>> Login(LoginDto dto)
        {
            var usuario = await _userManager.FindByEmailAsync(dto.Email);
            if (usuario == null)
            {
                return Unauthorized("Correo o contraseña incorrectos.");
            }

            var passwordValida = await _userManager.CheckPasswordAsync(usuario, dto.Password);
            if (!passwordValida)
            {
                return Unauthorized("Correo o contraseña incorrectos.");
            }

            var respuesta = await GenerarRespuestaAutenticacionAsync(usuario);
            return Ok(respuesta);
        }

        [HttpPost("refresh-token")]
        public async Task<ActionResult<AuthResponseDto>> RefreshToken(RefreshTokenRequestDto dto)
        {
            var tokenGuardado = await _context.RefreshTokens
                .Include(rt => rt.ApplicationUser)
                .FirstOrDefaultAsync(rt => rt.Token == dto.RefreshToken);

            if (tokenGuardado == null || tokenGuardado.Revocado || tokenGuardado.FechaExpiracion < DateTime.UtcNow)
            {
                return Unauthorized("Refresh token inválido o expirado.");
            }

            // Rotación: revocamos el token usado y emitimos uno nuevo
            tokenGuardado.Revocado = true;

            var respuesta = await GenerarRespuestaAutenticacionAsync(tokenGuardado.ApplicationUser);
            return Ok(respuesta);
        }

        [Authorize]
        [HttpPost("logout")]
        public async Task<IActionResult> Logout(RefreshTokenRequestDto dto)
        {
            var tokenGuardado = await _context.RefreshTokens
                .FirstOrDefaultAsync(rt => rt.Token == dto.RefreshToken);

            if (tokenGuardado == null)
            {
                return NotFound("Refresh token no encontrado.");
            }

            tokenGuardado.Revocado = true;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private async Task<AuthResponseDto> GenerarRespuestaAutenticacionAsync(ApplicationUser usuario)
        {
            var roles = await _userManager.GetRolesAsync(usuario);
            var (accessToken, expiracion) = _tokenService.GenerarToken(usuario.Id, usuario.Email!, roles);
            var refreshToken = _tokenService.GenerarRefreshToken();

            _context.RefreshTokens.Add(new RefreshToken
            {
                Token = refreshToken,
                ApplicationUserId = usuario.Id,
                FechaCreacion = DateTime.UtcNow,
                FechaExpiracion = DateTime.UtcNow.AddDays(DiasExpiracionRefreshToken),
                Revocado = false
            });

            await _context.SaveChangesAsync();

            return new AuthResponseDto
            {
                Token = accessToken,
                Expiracion = expiracion,
                RefreshToken = refreshToken,
                Email = usuario.Email!,
                Nombre = usuario.Nombre
            };
        }
    }
}