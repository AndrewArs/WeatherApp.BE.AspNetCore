namespace Application.Mediatr.Weather.Queries.One;
public class GetWeatherQuery : IRequestResult<WeatherResponse>
{
    public string? ProviderName { get; set; }
    public Guid? ProviderId { get; set; }
}
