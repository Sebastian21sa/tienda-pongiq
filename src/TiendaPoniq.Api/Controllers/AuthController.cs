using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TiendaPoniq.Application.DTOs;
using TiendaPoniq.Application.Interfaces;
using TiendaPoniq.Infrastructure.Identity;

namespace TiendaPoniq.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ITokenService _tokenService;

        public AuthController(UserManager<ApplicationUser> userManager, ITokenService tokenService)
        {
            _userManager = userManager;
            _tokenService = tokenService;
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

            var roles = await _userManager.GetRolesAsync(nuevoUsuario);
            var (token, expiracion) = _tokenService.GenerarToken(nuevoUsuario.Id, nuevoUsuario.Email!, roles);

            return Ok(new AuthResponseDto
            {
                Token = token,
                Expiracion = expiracion,
                Email = nuevoUsuario.Email!,
                Nombre = nuevoUsuario.Nombre
            });
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

            var roles = await _userManager.GetRolesAsync(usuario);
            var (token, expiracion) = _tokenService.GenerarToken(usuario.Id, usuario.Email!, roles);

            return Ok(new AuthResponseDto
            {
                Token = token,
                Expiracion = expiracion,
                Email = usuario.Email!,
                Nombre = usuario.Nombre
            });
        }
    }
}