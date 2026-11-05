using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TiendaPoniq.Application.DTOs;
using TiendaPoniq.Application.Interfaces;
using TiendaPoniq.Domain.Entities;

namespace TiendaPoniq.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductosController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductosController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<ActionResult<PagedResultDto<ProductoDto>>> GetAll(
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10,
            [FromQuery] int? categoriaId = null,
            [FromQuery] int? marcaId = null)
        {
            if (pageNumber < 1) pageNumber = 1;
            if (pageSize < 1 || pageSize > 100) pageSize = 10;

            var (productos, totalCount) = await _unitOfWork.Productos.GetPagedAsync(
                predicate: p =>
                    (!categoriaId.HasValue || p.CategoriaId == categoriaId.Value) &&
                    (!marcaId.HasValue || p.MarcaId == marcaId.Value) &&
                    p.Activo,
                pageNumber: pageNumber,
                pageSize: pageSize,
                include: query => query.Include(p => p.Categoria).Include(p => p.Marca));

            var items = productos.Select(p => new ProductoDto
            {
                Id = p.Id,
                Nombre = p.Nombre,
                Descripcion = p.Descripcion,
                Activo = p.Activo,
                CategoriaId = p.CategoriaId,
                CategoriaNombre = p.Categoria.Nombre,
                MarcaId = p.MarcaId,
                MarcaNombre = p.Marca.Nombre
            }).ToList();

            return Ok(new PagedResultDto<ProductoDto>
            {
                Items = items,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalCount = totalCount
            });
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductoDto>> GetById(int id)
        {
            var producto = await _unitOfWork.Productos.GetByIdAsync(id);

            if (producto == null)
            {
                return NotFound();
            }

            var categoria = await _unitOfWork.Categorias.GetByIdAsync(producto.CategoriaId);
            var marca = await _unitOfWork.Marcas.GetByIdAsync(producto.MarcaId);

            return Ok(new ProductoDto
            {
                Id = producto.Id,
                Nombre = producto.Nombre,
                Descripcion = producto.Descripcion,
                Activo = producto.Activo,
                CategoriaId = producto.CategoriaId,
                CategoriaNombre = categoria?.Nombre ?? string.Empty,
                MarcaId = producto.MarcaId,
                MarcaNombre = marca?.Nombre ?? string.Empty
            });
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult<ProductoDto>> Create(ProductoCreateDto dto)
        {
            var producto = new Producto
            {
                Nombre = dto.Nombre,
                Descripcion = dto.Descripcion,
                CategoriaId = dto.CategoriaId,
                MarcaId = dto.MarcaId
            };

            await _unitOfWork.Productos.AddAsync(producto);
            await _unitOfWork.SaveChangesAsync();

            var categoria = await _unitOfWork.Categorias.GetByIdAsync(producto.CategoriaId);
            var marca = await _unitOfWork.Marcas.GetByIdAsync(producto.MarcaId);

            return CreatedAtAction(nameof(GetById), new { id = producto.Id }, new ProductoDto
            {
                Id = producto.Id,
                Nombre = producto.Nombre,
                Descripcion = producto.Descripcion,
                Activo = producto.Activo,
                CategoriaId = producto.CategoriaId,
                CategoriaNombre = categoria?.Nombre ?? string.Empty,
                MarcaId = producto.MarcaId,
                MarcaNombre = marca?.Nombre ?? string.Empty
            });
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, ProductoCreateDto dto)
        {
            var producto = await _unitOfWork.Productos.GetByIdAsync(id);

            if (producto == null)
            {
                return NotFound();
            }

            producto.Nombre = dto.Nombre;
            producto.Descripcion = dto.Descripcion;
            producto.CategoriaId = dto.CategoriaId;
            producto.MarcaId = dto.MarcaId;

            _unitOfWork.Productos.Update(producto);
            await _unitOfWork.SaveChangesAsync();

            return NoContent();
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var producto = await _unitOfWork.Productos.GetByIdAsync(id);

            if (producto == null)
            {
                return NotFound();
            }

            producto.Activo = false;
            _unitOfWork.Productos.Update(producto);
            await _unitOfWork.SaveChangesAsync();

            return NoContent();
        }
    }
}