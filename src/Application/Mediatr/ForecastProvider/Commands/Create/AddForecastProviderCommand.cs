namespace Application.Mediatr.ForecastProvider.Commands.Create;

public class AddForecastProviderCommand : IRequestResult<EntityIdentifier>, IValidatableRequest
{
    public required string Name { get; set; }
    public required string Url { get; set; }
    public required string TemperaturePath { get; set; }
    public required string ForecastTemplatePath { get; set; }
    public string? KeyQueryParamName { get; set; }
}