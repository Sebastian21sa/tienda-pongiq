using FluentValidation.TestHelper;
using TiendaPoniq.Application.DTOs;
using TiendaPoniq.Application.Validators;
using Xunit;

namespace TiendaPoniq.Tests.Validators
{
    public class MarcaCreateDtoValidatorTests
    {
        private readonly MarcaCreateDtoValidator _validator = new MarcaCreateDtoValidator();

        [Fact]
        public void Debe_fallar_si_el_nombre_esta_vacio()
        {
            var dto = new MarcaCreateDto
            {
                Nombre = "",
                PaisOrigen = "Alemania"
            };

            var resultado = _validator.TestValidate(dto);

            resultado.ShouldHaveValidationErrorFor(m => m.Nombre);
        }

        [Fact]
        public void No_debe_fallar_si_el_paisOrigen_es_nulo()
        {
            var dto = new MarcaCreateDto
            {
                Nombre = "Butterfly",
                PaisOrigen = null
            };

            var resultado = _validator.TestValidate(dto);

            resultado.ShouldNotHaveAnyValidationErrors();
        }

        [Fact]
        public void No_debe_fallar_si_todos_los_datos_son_validos()
        {
            var dto = new MarcaCreateDto
            {
                Nombre = "Butterfly",
                PaisOrigen = "Japon"
            };

            var resultado = _validator.TestValidate(dto);

            resultado.ShouldNotHaveAnyValidationErrors();
        }
    }
}