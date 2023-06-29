namespace WebApi.Dtos;

public record WeatherForecastDto(string Provider, float Temperature, string Weather, DateTimeOffset UpdatedAt);