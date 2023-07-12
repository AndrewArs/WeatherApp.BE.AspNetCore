namespace Application.Mediatr.ForecastProvider.Shared;

public record ForecastProviderResponse(
    Guid Id,
    DateTimeOffset CreatedAt,
    DateTimeOffset UpdatedAt,
    string Name,
    string Slug,
    string Url,
    string TemperaturePath,
    string ForecastTemplatePath,
    string? KeyQueryParamName);