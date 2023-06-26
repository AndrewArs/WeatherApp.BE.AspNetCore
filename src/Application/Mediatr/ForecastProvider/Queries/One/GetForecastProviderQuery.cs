using Application.Mediatr.ForecastProvider.Shared;

namespace Application.Mediatr.ForecastProvider.Queries.One;

public class GetForecastProviderQuery : IRequestResult<ForecastProviderResponse>, IValidatableRequest
{
    public Guid? Id { get; set; }

    public string? Name { get; set; }
}