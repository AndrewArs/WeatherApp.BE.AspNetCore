using Application.Mediatr.WeatherForecast.Queries;
using Application.Mediatr.WeatherForecast.Queries.All;

namespace WebApi.Mappings;

public static class WeatherForecastsMappings
{
    public static WeatherForecastsAllDto ToDto(this WeatherForecastsResponse domain)
        => new(domain.Data.Select(x => x.ToDto()).ToArray(), domain.Errors.Select(x => x.ToDto()).ToArray());

    public static WeatherForecastDto ToDto(this WeatherForecastResponse domain)
        => new(domain.Provider, domain.Temperature, domain.Weather, domain.UpdatedAt);

    public static WeatherForecastsAllDto.FailDto ToDto(this WeatherForecastsResponse.FailResponse domain)
        => new(domain.ProviderName, domain.Error);
}