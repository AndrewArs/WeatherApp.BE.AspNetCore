using Application.Mediatr.ForecastProvider.Shared;

namespace Application.Mediatr.ForecastProvider.Queries.One;

public class GetForecastProviderQuery : IRequestResult<ForecastProviderResponse>
{
    public Guid? Id { get; set; }

    public string? Slug { get; set; }
}