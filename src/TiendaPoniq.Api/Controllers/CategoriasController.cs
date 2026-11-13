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
    public class CategoriasController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<CategoriaCreateDto> _validator;

        public CategoriasController(IUnitOfWork unitOfWork, IValidator<CategoriaCreateDto> validator)
        {
            _unitOfWork = unitOfWork;
            _validator = validator;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoriaDto>>> GetAll()
        {
            var categorias = await _unitOfWork.Categorias.GetAllAsync();

            var resultado = categorias.Select(c => new CategoriaDto
            {
                Id = c.Id,
                Nombre = c.Nombre,
                Descripcion = c.Descripcion
            });

            return Ok(resultado);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CategoriaDto>> GetById(int id)
        {
            var categoria = await _unitOfWork.Categorias.GetByIdAsync(id);

            if (categoria == null)
            {
                return NotFound();
            }

            return Ok(new CategoriaDto
            {
                Id = categoria.Id,
                Nombre = categoria.Nombre,
                Descripcion = categoria.Descripcion
            });
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult<CategoriaDto>> Create(CategoriaCreateDto dto)
        {
            var validacion = await _validator.ValidateAsync(dto);
            if (!validacion.IsValid)
            {
                return BadRequest(validacion.Errors.Select(e => e.ErrorMessage));
            }

            var categoria = new Categoria
            {
                Nombre = dto.Nombre,
                Descripcion = dto.Descripcion
            };

            await _unitOfWork.Categorias.AddAsync(categoria);
            await _unitOfWork.SaveChangesAsync();

            var resultado = new CategoriaDto
            {
                Id = categoria.Id,
                Nombre = categoria.Nombre,
                Descripcion = categoria.Descripcion
            };

            return CreatedAtAction(nameof(GetById), new { id = categoria.Id }, resultado);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, CategoriaCreateDto dto)
        {
            var validacion = await _validator.ValidateAsync(dto);
            if (!validacion.IsValid)
            {
                return BadRequest(validacion.Errors.Select(e => e.ErrorMessage));
            }

            var categoria = await _unitOfWork.Categorias.GetByIdAsync(id);

            if (categoria == null)
            {
                return NotFound();
            }

            categoria.Nombre = dto.Nombre;
            categoria.Descripcion = dto.Descripcion;

            _unitOfWork.Categorias.Update(categoria);
            await _unitOfWork.SaveChangesAsync();

            return NoContent();
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var categoria = await _unitOfWork.Categorias.GetByIdAsync(id);

            if (categoria == null)
            {
                return NotFound();
            }

            _unitOfWork.Categorias.Remove(categoria);
            await _unitOfWork.SaveChangesAsync();

            return NoContent();
        }
    }
}