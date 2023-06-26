using FluentValidation;

namespace Application.Mediatr.Weather.Queries.One;

public class GetWeatherQueryValidator : AbstractValidator<GetWeatherQuery>
{
    public GetWeatherQueryValidator()
    {
        RuleFor(x => x)
            .Custom((query, context) =>
            {
                if (query.ProviderId is null && string.IsNullOrEmpty(query.ProviderName))
                {
                    context.AddFailure($"At least one identifier should be specified {nameof(query.ProviderId)} or {nameof(query.ProviderName)}");
                }
            });
    }
}