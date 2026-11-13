using FluentValidation;
using TiendaPoniq.Application.DTOs;

namespace TiendaPoniq.Application.Validators
{
    public class CategoriaCreateDtoValidator : AbstractValidator<CategoriaCreateDto>
    {
        public CategoriaCreateDtoValidator()
        {
            RuleFor(c => c.Nombre)
                .NotEmpty().WithMessage("El nombre de la categoría es obligatorio.")
                .MaximumLength(100).WithMessage("El nombre no puede superar los 100 caracteres.");

            RuleFor(c => c.Descripcion)
                .MaximumLength(300).WithMessage("La descripción no puede superar los 300 caracteres.");
        }
    }
}