using Domain.Common;

namespace Domain.Entities;
public class ForecastProviderSettings : BaseAuditableEntity
{
    public required string Name { get; set; }
    public required string Url { get; set; }
    public required string TemperaturePath { get; set; }
    public required string ForecastTemplatePath { get; set; }
    public string? KeyQueryParamName { get; set; }

}
