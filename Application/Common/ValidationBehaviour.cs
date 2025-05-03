using System;
using FluentValidation;
using MediatR;

namespace Application.Common;

public class ValidationBehaviour<TRequest, TResponse>(IValidator<TRequest>? validator = null)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (validator == null) return await next();

        var results = await validator.ValidateAsync(request, cancellationToken);

        if (!results.IsValid)
        {
            throw new ValidationException(results.Errors);

        }

        return await next();
    }
}
