using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedBrowTest.Core.Application.Features.Usuario.Update
{
    public class UpdateCommandValidator : AbstractValidator<UpdateCommand>
    {
        public UpdateCommandValidator()
        {
            RuleFor(v => v.IdUsuario)
                .NotEmpty().WithMessage("El IdUsuario es requerido");

            // validamos que el nombre no sea nulo, ni vacio, ni mayor a 200 caracteres
            RuleFor(p => p.Nombre)
                .NotEmpty().WithMessage("{PropertyName} no puede ser nulo.")
                .NotNull()
                .MaximumLength(200).WithMessage("{PropertyName} no puede ser mayor a {MaxLength} caracteres.");
            // validamos que el email sea un email valido y no sea mayor a 320 caracteres
            RuleFor(p => p.Email)
                .NotEmpty().WithMessage("{PropertyName} no puede ser nulo.")
                .NotNull()
                .EmailAddress().WithMessage("{PropertyName} no es un email valido.")
                .MaximumLength(320).WithMessage("{PropertyName} no puede ser mayor a {MaxLength} caracteres.");
            // validamos que el password no sea nulo, ni vacio
            RuleFor(p => p.Password)
                .NotEmpty().WithMessage("{PropertyName} no puede ser nulo.")
                .NotNull();
            // validamos que el campo entero edad tenga valores y no sea menor a cero
            RuleFor(p => p.Edad)
                .NotNull().WithMessage("{PropertyName} no puede ser nulo.")
                .GreaterThanOrEqualTo(0).WithMessage("{PropertyName} no puede ser menor a {ComparisonValue}.");

        }
    }
}
