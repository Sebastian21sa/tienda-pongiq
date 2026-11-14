using FluentValidation.TestHelper;
using TiendaPoniq.Application.DTOs;
using TiendaPoniq.Application.Validators;
using Xunit;

namespace TiendaPoniq.Tests.Validators
{
    public class ProductoCreateDtoValidatorTests
    {
        private readonly ProductoCreateDtoValidator _validator = new ProductoCreateDtoValidator();

        [Fact]
        public void Debe_fallar_si_el_nombre_esta_vacio()
        {
            var dto = new ProductoCreateDto
            {
                Nombre = "",
                Descripcion = "Descripcion valida",
                CategoriaId = 1,
                MarcaId = 1
            };

            var resultado = _validator.TestValidate(dto);

            resultado.ShouldHaveValidationErrorFor(p => p.Nombre);
        }

        [Fact]
        public void Debe_fallar_si_la_categoriaId_es_cero()
        {
            var dto = new ProductoCreateDto
            {
                Nombre = "Producto valido",
                Descripcion = "Descripcion valida",
                CategoriaId = 0,
                MarcaId = 1
            };

            var resultado = _validator.TestValidate(dto);

            resultado.ShouldHaveValidationErrorFor(p => p.CategoriaId);
        }

        [Fact]
        public void Debe_fallar_si_la_marcaId_es_negativa()
        {
            var dto = new ProductoCreateDto
            {
                Nombre = "Producto valido",
                Descripcion = "Descripcion valida",
                CategoriaId = 1,
                MarcaId = -1
            };

            var resultado = _validator.TestValidate(dto);

            resultado.ShouldHaveValidationErrorFor(p => p.MarcaId);
        }

        [Fact]
        public void No_debe_fallar_si_todos_los_datos_son_validos()
        {
            var dto = new ProductoCreateDto
            {
                Nombre = "Butterfly Timo Boll ALC",
                Descripcion = "Pala ofensiva de fibra de carbono",
                CategoriaId = 1,
                MarcaId = 1
            };

            var resultado = _validator.TestValidate(dto);

            resultado.ShouldNotHaveAnyValidationErrors();
        }

        [Theory]
        [InlineData(151)]
        [InlineData(200)]
        public void Debe_fallar_si_el_nombre_supera_el_maximo_de_caracteres(int longitud)
        {
            var dto = new ProductoCreateDto
            {
                Nombre = new string('A', longitud),
                Descripcion = "Descripcion valida",
                CategoriaId = 1,
                MarcaId = 1
            };

            var resultado = _validator.TestValidate(dto);

            resultado.ShouldHaveValidationErrorFor(p => p.Nombre);
        }
    }
}