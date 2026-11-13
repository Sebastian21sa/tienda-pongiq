using FluentValidation;
using TiendaPoniq.Application.DTOs;

namespace TiendaPoniq.Application.Validators
{
    public class ProductoCreateDtoValidator : AbstractValidator<ProductoCreateDto>
    {
        public ProductoCreateDtoValidator()
        {
            RuleFor(p => p.Nombre)
                .NotEmpty().WithMessage("El nombre del producto es obligatorio.")
                .MaximumLength(150).WithMessage("El nombre no puede superar los 150 caracteres.");

            RuleFor(p => p.Descripcion)
                .NotEmpty().WithMessage("La descripción es obligatoria.")
                .MaximumLength(500).WithMessage("La descripción no puede superar los 500 caracteres.");

            RuleFor(p => p.CategoriaId)
                .GreaterThan(0).WithMessage("Debes indicar una categoría válida.");

            RuleFor(p => p.MarcaId)
                .GreaterThan(0).WithMessage("Debes indicar una marca válida.");
        }
    }
}