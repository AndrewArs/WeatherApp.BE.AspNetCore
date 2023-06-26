using Application.Mediatr.ForecastProvider.Shared;
using Microsoft.EntityFrameworkCore;

namespace Application.Mediatr.ForecastProvider.Queries.All;

public class GetForecastProviderAllHandler : IRequestResultHandler<GetForecastProviderAllQuery, ListOf<ForecastProviderResponse>>
{
    private readonly IDatabaseContext _databaseContext;
    private readonly IUrlService _urlService;

    public GetForecastProviderAllHandler(IDatabaseContext databaseContext, IUrlService urlService)
    {
        _databaseContext = databaseContext;
        _urlService = urlService;
    }

    public async Task<Result<ListOf<ForecastProviderResponse>>> Handle(GetForecastProviderAllQuery request, CancellationToken cancellationToken)
    {
        var providers = await _databaseContext.ForecastProviderSettings
            .AsNoTracking()
            .ToListAsync(cancellationToken);

        providers.ForEach(x =>
        {
            if (!string.IsNullOrEmpty(x.KeyQueryParamName))
            {
                x.Url = _urlService.MaskUrlQueryParams(x.Url, x.KeyQueryParamName);
            }
        });

        var data = providers.Select(x => new ForecastProviderResponse(
            x.Id,
            x.CreatedAt,
            x.UpdatedAt,
            x.Name,
            x.Url,
            x.TemperaturePath,
            x.ForecastTemplatePath,
            x.KeyQueryParamName)).ToArray();

        return new ListOf<ForecastProviderResponse>(data);
    }
}