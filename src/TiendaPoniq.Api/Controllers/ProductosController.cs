using FluentValidation;
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
        private readonly IValidator<ProductoCreateDto> _validator;

        public ProductosController(IUnitOfWork unitOfWork, IValidator<ProductoCreateDto> validator)
        {
            _unitOfWork = unitOfWork;
            _validator = validator;
        }

        [HttpGet]
        public async Task<ActionResult<PagedResultDto<ProductoDto>>> GetAll(
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10,
            [FromQuery] int? categoriaId = null,
            [FromQuery] int? marcaId = null,
            [FromQuery] string? busqueda = null,
            [FromQuery] decimal? precioMin = null,
            [FromQuery] decimal? precioMax = null)
        {
            if (pageNumber < 1) pageNumber = 1;
            if (pageSize < 1 || pageSize > 100) pageSize = 10;

            var (productos, totalCount) = await _unitOfWork.Productos.GetPagedAsync(
                predicate: p =>
                    (!categoriaId.HasValue || p.CategoriaId == categoriaId.Value) &&
                    (!marcaId.HasValue || p.MarcaId == marcaId.Value) &&
                    (string.IsNullOrEmpty(busqueda) ||
                        p.Nombre.ToLower().Contains(busqueda.ToLower()) ||
                        p.Descripcion.ToLower().Contains(busqueda.ToLower())) &&
                    ((!precioMin.HasValue && !precioMax.HasValue) ||
                        p.Variantes.Any(v =>
                            (!precioMin.HasValue || v.Precio >= precioMin.Value) &&
                            (!precioMax.HasValue || v.Precio <= precioMax.Value))) &&
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

        [HttpGet("{id}/detalle")]
        public async Task<ActionResult<ProductoDetalleDto>> GetDetalle(int id)
        {
            var producto = await _unitOfWork.Productos.GetFirstOrDefaultAsync(
                p => p.Id == id,
                include: query => query
                    .Include(p => p.Categoria)
                    .Include(p => p.Marca)
                    .Include(p => p.Variantes));

            if (producto == null)
            {
                return NotFound();
            }

            var resultado = new ProductoDetalleDto
            {
                Id = producto.Id,
                Nombre = producto.Nombre,
                Descripcion = producto.Descripcion,
                Activo = producto.Activo,
                CategoriaId = producto.CategoriaId,
                CategoriaNombre = producto.Categoria.Nombre,
                MarcaId = producto.MarcaId,
                MarcaNombre = producto.Marca.Nombre,
                Variantes = producto.Variantes.Select(v => new VarianteProductoDto
                {
                    Id = v.Id,
                    Sku = v.Sku,
                    Atributo = v.Atributo,
                    Precio = v.Precio,
                    Stock = v.Stock
                }).ToList()
            };

            return Ok(resultado);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult<ProductoDto>> Create(ProductoCreateDto dto)
        {
            var validacion = await _validator.ValidateAsync(dto);
            if (!validacion.IsValid)
            {
                return BadRequest(validacion.Errors.Select(e => e.ErrorMessage));
            }

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
            var validacion = await _validator.ValidateAsync(dto);
            if (!validacion.IsValid)
            {
                return BadRequest(validacion.Errors.Select(e => e.ErrorMessage));
            }

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