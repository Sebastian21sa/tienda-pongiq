using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TiendaPoniq.Application.DTOs;
using TiendaPoniq.Application.Interfaces;
using TiendaPoniq.Domain.Entities;

namespace TiendaPoniq.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MarcasController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<MarcaCreateDto> _validator;

        public MarcasController(IUnitOfWork unitOfWork, IValidator<MarcaCreateDto> validator)
        {
            _unitOfWork = unitOfWork;
            _validator = validator;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MarcaDto>>> GetAll()
        {
            var marcas = await _unitOfWork.Marcas.FindAsync(m => m.Activa);

            var resultado = marcas.Select(m => new MarcaDto
            {
                Id = m.Id,
                Nombre = m.Nombre,
                PaisOrigen = m.PaisOrigen,
                Activa = m.Activa
            });

            return Ok(resultado);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MarcaDto>> GetById(int id)
        {
            var marca = await _unitOfWork.Marcas.GetByIdAsync(id);

            if (marca == null)
            {
                return NotFound();
            }

            return Ok(new MarcaDto
            {
                Id = marca.Id,
                Nombre = marca.Nombre,
                PaisOrigen = marca.PaisOrigen,
                Activa = marca.Activa
            });
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult<MarcaDto>> Create(MarcaCreateDto dto)
        {
            var validacion = await _validator.ValidateAsync(dto);
            if (!validacion.IsValid)
            {
                return BadRequest(validacion.Errors.Select(e => e.ErrorMessage));
            }

            var marca = new Marca
            {
                Nombre = dto.Nombre,
                PaisOrigen = dto.PaisOrigen
            };

            await _unitOfWork.Marcas.AddAsync(marca);
            await _unitOfWork.SaveChangesAsync();

            var resultado = new MarcaDto
            {
                Id = marca.Id,
                Nombre = marca.Nombre,
                PaisOrigen = marca.PaisOrigen,
                Activa = marca.Activa
            };

            return CreatedAtAction(nameof(GetById), new { id = marca.Id }, resultado);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, MarcaCreateDto dto)
        {
            var validacion = await _validator.ValidateAsync(dto);
            if (!validacion.IsValid)
            {
                return BadRequest(validacion.Errors.Select(e => e.ErrorMessage));
            }

            var marca = await _unitOfWork.Marcas.GetByIdAsync(id);

            if (marca == null)
            {
                return NotFound();
            }

            marca.Nombre = dto.Nombre;
            marca.PaisOrigen = dto.PaisOrigen;

            _unitOfWork.Marcas.Update(marca);
            await _unitOfWork.SaveChangesAsync();

            return NoContent();
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var marca = await _unitOfWork.Marcas.GetByIdAsync(id);

            if (marca == null)
            {
                return NotFound();
            }

            marca.Activa = false;
            _unitOfWork.Marcas.Update(marca);
            await _unitOfWork.SaveChangesAsync();

            return NoContent();
        }
    }
}