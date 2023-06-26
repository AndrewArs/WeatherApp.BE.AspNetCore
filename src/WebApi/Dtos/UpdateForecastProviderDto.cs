namespace WebApi.Dtos;

public record UpdateForecastProviderDto(
    string? Name,
    string? Url,
    string? TemperaturePath,
    string? ForecastTemplatePath,
    string? KeyQueryParamName);