using FluentValidation;
using TiendaPoniq.Application.DTOs;

namespace TiendaPoniq.Application.Validators
{
    public class MarcaCreateDtoValidator : AbstractValidator<MarcaCreateDto>
    {
        public MarcaCreateDtoValidator()
        {
            RuleFor(m => m.Nombre)
                .NotEmpty().WithMessage("El nombre de la marca es obligatorio.")
                .MaximumLength(100).WithMessage("El nombre no puede superar los 100 caracteres.");

            RuleFor(m => m.PaisOrigen)
                .MaximumLength(100).WithMessage("El país de origen no puede superar los 100 caracteres.");
        }
    }
}