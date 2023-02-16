
using FluentValidation;
using MediatR;
using ValidationException = CleanArchitecture.Application.Exceptions.ValidationException;

namespace CleanArchitecture.Application.Behaviours
{
    public class ValidationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidationBehaviour(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (_validators.Any())
            {
                var context = new ValidationContext<TRequest>(request);
                //ejecuta las validaciones en el pipeline
                var validationResults = await Task.WhenAll(_validators.Select(v => v.ValidateAsync(context, cancellationToken)));

               var failures = validationResults.SelectMany(r => r.Errors).Where(f => f != null).ToList();
                //si hay errores de validacion lanza una exception
                if(failures.Count != 0)
                {
                    throw new ValidationException(failures);                }
            }

            return await next();
        }
    }
}
