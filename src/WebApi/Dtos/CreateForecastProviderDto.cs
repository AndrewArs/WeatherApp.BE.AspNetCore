namespace WebApi.Dtos;

public record CreateForecastProviderDto(
    string Name,
    string Url,
    string TemperaturePath,
    string ForecastTemplatePath,
    string? KeyQueryParamName);