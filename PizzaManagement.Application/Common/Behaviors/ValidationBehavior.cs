using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ErrorOr;
using FluentValidation;
using MediatR;

namespace PizzaManagement.Application.Common.Behaviors
{
    public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    where TResponse : IErrorOr
    {
        private readonly IValidator<TRequest>? _validator;

        public ValidationBehavior(IValidator<TRequest>? validator = null)
        {
            _validator = validator;
        }

        public async Task<TResponse> Handle
        (
            TRequest request,
            RequestHandlerDelegate<TResponse> next,
            CancellationToken cancellationToken
        )
        {
            /* === BEFORE THE HANDLER IS CALLED === */

            // If no validators
            if (_validator is null)
            {
                return await next();
            }

            var validationResult = await _validator.ValidateAsync(request, cancellationToken);

            // Call the handler if the validation is successfull
            /* === BEFORE THE HANDLER IS CALLED === */

            if (validationResult.IsValid)
            {
                return await next();
            }

            /* === AFTER THE HANDLER IS CALLED === */

            // Convert the flunent validation errors to an ErrorOr error list
            var errors = validationResult.Errors
                .ConvertAll(
                    validationError => Error.Validation(
                        validationError.PropertyName,
                        validationError.ErrorMessage
                    )
                );

            /* === AFTER THE HANDLER IS CALLED === */

            return (dynamic)errors;
        }
    }
}