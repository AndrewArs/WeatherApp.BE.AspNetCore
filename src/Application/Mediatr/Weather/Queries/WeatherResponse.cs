namespace Application.Mediatr.Weather.Queries;
public record WeatherResponse(string Provider, float Temperature, string Weather, DateTimeOffset UpdatedAt);
