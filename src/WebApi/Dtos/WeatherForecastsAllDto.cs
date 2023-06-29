namespace WebApi.Dtos;

public record WeatherForecastsAllDto(WeatherForecastDto[] Data, WeatherForecastsAllDto.FailDto[] Errors)
{
    public record FailDto(string ProviderName, string Error);
}
