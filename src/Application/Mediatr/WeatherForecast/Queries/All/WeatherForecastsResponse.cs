namespace Application.Mediatr.WeatherForecast.Queries.All;

public record WeatherForecastsResponse(WeatherForecastResponse[] Data, WeatherForecastsResponse.FailResponse[] Errors)
{
    public record FailResponse(string ProviderName, string Error);
}
