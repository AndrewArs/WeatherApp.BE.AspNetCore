using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Mediatr.WeatherForecast.Queries.One;
public class GetWeatherForecastHandler : IRequestResultHandler<GetWeatherForecastQuery, WeatherForecastResponse>
{
    private readonly IHttpClientFactory _clientFactory;
    private readonly IDatabaseContext _databaseContext;
    private readonly ITemplateService _templateService;
    private readonly IJsonParserService _jsonParserService;
    private readonly IDateTimeService _dateTimeService;

    public GetWeatherForecastHandler(
        IHttpClientFactory clientFactory,
        IDatabaseContext databaseContext,
        ITemplateService templateService,
        IJsonParserService jsonParserService,
        IDateTimeService dateTimeService)
    {
        _clientFactory = clientFactory;
        _databaseContext = databaseContext;
        _templateService = templateService;
        _jsonParserService = jsonParserService;
        _dateTimeService = dateTimeService;
    }

    public async Task<Result<WeatherForecastResponse>> Handle(GetWeatherForecastQuery request, CancellationToken cancellationToken)
    {
        var query = _databaseContext.ForecastProviderSettings.AsNoTracking();

        if (request.ProviderId.HasValue)
        {
            query = query.Where(x => x.Id == request.ProviderId);
        }

        if (!string.IsNullOrEmpty(request.ProviderName))
        {
            query = query.Where(x => request.ProviderName.ToLower().Equals(x.Name.ToLower()));
        }

        var provider = await query.FirstOrDefaultAsync(cancellationToken);

        if (provider == null) return Error.NotFound<ForecastProviderSettings>();

        var httpClient = _clientFactory.CreateClient(provider.Name);

        var providerResponse = await httpClient.GetAsync(provider.Url, cancellationToken);

        if(!providerResponse.IsSuccessStatusCode) return Error.Unhandled($"Provider returned unsuccessful status code {providerResponse.StatusCode}");

        var json = await providerResponse.Content.ReadAsStringAsync(cancellationToken);
        
        return new WeatherForecastResponse(
            provider.Name,
            _jsonParserService.GetValueByPath<float>(json, provider.TemperaturePath),
            _templateService.BuildTemplateFromJson(provider.ForecastTemplatePath, json),
            _dateTimeService.UtcNow);
    }
}
