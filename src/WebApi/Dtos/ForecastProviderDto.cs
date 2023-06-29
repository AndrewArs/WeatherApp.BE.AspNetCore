namespace WebApi.Dtos;

public record ForecastProviderDto(
    Guid Id,
    DateTimeOffset CreatedAt,
    DateTimeOffset UpdatedAt,
    string Name,
    string Url,
    string TemperaturePath,
    string ForecastTemplatePath,
    string? KeyQueryParamName);