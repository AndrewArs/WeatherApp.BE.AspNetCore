using FluentValidation;

namespace Application.Mediatr.WeatherForecast.Queries.One;

public class GetWeatherForecastQueryValidator : AbstractValidator<GetWeatherForecastQuery>
{
    public GetWeatherForecastQueryValidator()
    {
        RuleFor(x => x)
            .Custom((query, context) =>
            {
                if (query.ProviderId is null && string.IsNullOrEmpty(query.ProviderSlug))
                {
                    context.AddFailure($"At least one identifier should be specified {nameof(query.ProviderId)} or {nameof(query.ProviderSlug)}");
                }
            });
    }
}