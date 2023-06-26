namespace WebApi.Dtos;

public record WeatherDto(string Provider, float Temperature, string Weather, DateTimeOffset UpdatedAt);