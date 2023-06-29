namespace Application.Mediatr.WeatherForecast.Queries.One;
public class GetWeatherForecastQuery : IRequestResult<WeatherForecastResponse>
{
    public string? ProviderName { get; set; }
    public Guid? ProviderId { get; set; }
}
