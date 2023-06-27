namespace WebApi.Dtos;

public record UpdateForecastProviderDto(
    string? Name = null,
    string? Url = null,
    string? TemperaturePath = null,
    string? ForecastTemplatePath = null,
    string? KeyQueryParamName = null);