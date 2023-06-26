namespace WebApi.Dtos;

public record WeatherProviderDto(
    Guid Id,
    DateTimeOffset CreatedAt,
    DateTimeOffset UpdatedAt,
    string Name,
    string Url,
    string TemperaturePath,
    string ForecastTemplatePath,
    string? KeyQueryParamName);