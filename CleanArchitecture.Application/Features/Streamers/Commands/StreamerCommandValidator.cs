using FluentValidation;


namespace CleanArchitecture.Application.Features.Streamers.Commands
{
    public class StreamerCommandValidator : AbstractValidator<StreamerCommand>
    {
        public StreamerCommandValidator()
        {
            RuleFor(p => p.Nombre)
                .NotEmpty().WithMessage("{Nombre} no puede estar en blanco")
                .NotNull()
                .MaximumLength(50).WithMessage("{Nombre} no puede exceder los 50 caracteres");

            RuleFor(p => p.Url)
                .NotEmpty().WithMessage("La {Url} no puede estar en blanco");
        }
    }
}
