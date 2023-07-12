namespace Application.Mediatr.WeatherForecast.Queries.One;
public class GetWeatherForecastQuery : IRequestResult<WeatherForecastResponse>
{
    public string? ProviderSlug { get; set; }
    public Guid? ProviderId { get; set; }
}
