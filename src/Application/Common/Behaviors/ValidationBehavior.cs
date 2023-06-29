using FluentValidation;
using MediatR;

namespace Application.Common.Behaviors;

public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, Result<TResponse>> 
    where TRequest : notnull
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }

    public async Task<Result<TResponse>> Handle(TRequest request, RequestHandlerDelegate<Result<TResponse>> next, CancellationToken cancellationToken)
    {
        if (!_validators.Any())
        {
            return await next.Invoke();
        }

        var results = await Task.WhenAll(_validators.Select(x => x.ValidateAsync(request, cancellationToken)));
        
        var errors = results
            .Where(x => !x.IsValid)
            .SelectMany(x => x.Errors)
            .Where(x => x != null)
            .GroupBy(
                x => x.PropertyName,
                x => x.ErrorMessage,
                (propertyName, errorMessages) => $"{propertyName}: {string.Join(',', errorMessages.Distinct())}")
            .Where(x => !string.IsNullOrEmpty(x))
            .ToArray();

        if (errors.Any())
        {
            var errorMessage = errors.Length == 1 ? errors.First() : string.Join($";{Environment.NewLine}", errors);
            return Error.ValidationFailed(errorMessage);
        }

        return await next.Invoke();
    }
}
