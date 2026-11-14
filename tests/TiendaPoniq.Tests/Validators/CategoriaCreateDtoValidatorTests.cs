using FluentValidation.TestHelper;
using TiendaPoniq.Application.DTOs;
using TiendaPoniq.Application.Validators;
using Xunit;

namespace TiendaPoniq.Tests.Validators
{
    public class CategoriaCreateDtoValidatorTests
    {
        private readonly CategoriaCreateDtoValidator _validator = new CategoriaCreateDtoValidator();

        [Fact]
        public void Debe_fallar_si_el_nombre_esta_vacio()
        {
            var dto = new CategoriaCreateDto
            {
                Nombre = "",
                Descripcion = "Una descripcion"
            };

            var resultado = _validator.TestValidate(dto);

            resultado.ShouldHaveValidationErrorFor(c => c.Nombre);
        }

        [Fact]
        public void No_debe_fallar_si_la_descripcion_es_nula()
        {
            var dto = new CategoriaCreateDto
            {
                Nombre = "Pelotas",
                Descripcion = null
            };

            var resultado = _validator.TestValidate(dto);

            resultado.ShouldNotHaveAnyValidationErrors();
        }

        [Fact]
        public void No_debe_fallar_si_todos_los_datos_son_validos()
        {
            var dto = new CategoriaCreateDto
            {
                Nombre = "Pelotas",
                Descripcion = "Pelotas de competicion y entrenamiento"
            };

            var resultado = _validator.TestValidate(dto);

            resultado.ShouldNotHaveAnyValidationErrors();
        }
    }
}