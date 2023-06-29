namespace Application.Mediatr.WeatherForecast.Queries;
public record WeatherForecastResponse(string Provider, float Temperature, string Weather, DateTimeOffset UpdatedAt);
