using Application.Mediatr.ForecastProvider.Shared;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Mediatr.ForecastProvider.Queries.One;

public class GetForecastProviderHandler : IRequestResultHandler<GetForecastProviderQuery, ForecastProviderResponse>
{
    private readonly IDatabaseContext _databaseContext;
    private readonly IUrlService _urlService;

    public GetForecastProviderHandler(IUrlService urlService, IDatabaseContext databaseContext)
    {
        _urlService = urlService;
        _databaseContext = databaseContext;
    }

    public async Task<Result<ForecastProviderResponse>> Handle(GetForecastProviderQuery request, CancellationToken cancellationToken)
    {
        var query = _databaseContext.ForecastProviderSettings.AsNoTracking();

        if (request.Id.HasValue)
        {
            query = query.Where(x => x.Id == request.Id);
        }
        if (!string.IsNullOrEmpty(request.Slug))
        {
            query = query.Where(x => request.Slug.ToLower().Equals(x.Slug.ToLower()));
        }

        var provider = await query.FirstOrDefaultAsync(cancellationToken);

        if (provider == null) return Error.NotFound<ForecastProviderSettings>();

        if (request.HideUrlKey && !string.IsNullOrEmpty(provider.KeyQueryParamName))
        {
            provider.Url = _urlService.MaskUrlQueryParams(provider.Url, provider.KeyQueryParamName);
        }

        return new ForecastProviderResponse(
            provider.Id,
            provider.CreatedAt,
            provider.UpdatedAt,
            provider.Name,
            provider.Slug,
            provider.Url,
            provider.TemperaturePath,
            provider.ForecastTemplatePath,
            provider.KeyQueryParamName);
    }
}