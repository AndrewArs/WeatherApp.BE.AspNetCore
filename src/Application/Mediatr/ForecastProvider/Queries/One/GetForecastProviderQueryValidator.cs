using FluentValidation;

namespace Application.Mediatr.ForecastProvider.Queries.One;

public class GetForecastProviderQueryValidator : AbstractValidator<GetForecastProviderQuery>
{
    public GetForecastProviderQueryValidator()
    {
        RuleFor(x => x)
            .Custom((query, context) =>
            {
                if (query.Id is null && string.IsNullOrEmpty(query.Slug))
                {
                    context.AddFailure($"At least one identifier should be specified {nameof(query.Id)} or {nameof(query.Slug)}");
                }
            });
    }
}