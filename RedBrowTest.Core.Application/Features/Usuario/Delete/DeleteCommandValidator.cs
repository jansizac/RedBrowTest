using FluentValidation;

namespace RedBrowTest.Core.Application.Features.Usuario.Delete
{
    public class DeleteCommandValidator : AbstractValidator<DeleteCommand>
    {
        public DeleteCommandValidator()
        {
            RuleFor(v => v.Id)
                .NotEmpty().WithMessage("El Id es requerido");
        }
    }
}
