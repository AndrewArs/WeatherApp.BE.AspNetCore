using Application.Mediatr.ForecastProvider.Shared;

namespace Application.Mediatr.ForecastProvider.Commands.Update;

public class UpdateForecastProviderCommand : IRequestResult<ForecastProviderResponse>
{
    public required Guid Id { get; set; }
    public string? Name { get; set; }
    public string? Url { get; set; }
    public string? TemperaturePath { get; set; }
    public string? ForecastTemplatePath { get; set; }
    public string? KeyQueryParamName { get; set; }
}