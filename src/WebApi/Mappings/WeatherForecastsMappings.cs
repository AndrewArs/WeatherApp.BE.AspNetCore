using Application.Mediatr.Weather.Queries;

namespace WebApi.Mappings;

public static class WeatherForecastsMappings
{
    public static ListOfDto<WeatherDto> ToDto(this ListOf<WeatherResponse> domain)
        => new(domain.Data.Select(x => x.ToDto()).ToList());

    public static WeatherDto ToDto(this WeatherResponse domain)
        => new(domain.Provider, domain.Temperature, domain.Weather, domain.UpdatedAt);
}