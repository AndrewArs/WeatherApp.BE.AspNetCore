using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Mediatr.Weather.Queries.All;

public class GetWeatherAllHandler : IRequestResultHandler<GetWeatherAllQuery, ListOf<WeatherResponse>>
{
    private readonly IHttpClientFactory _clientFactory;
    private readonly IDatabaseContext _databaseContext;
    private readonly ITemplateService _templateService;
    private readonly IJsonParserService _jsonParserService;
    private readonly IDateTimeService _dateTimeService;

    public GetWeatherAllHandler(
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

    public async Task<Result<ListOf<WeatherResponse>>> Handle(GetWeatherAllQuery request, CancellationToken cancellationToken)
    {
        var providers = await _databaseContext.ForecastProviderSettings
            .AsNoTracking()
            .ToListAsync(cancellationToken);

        if (!providers.Any()) return Error.NotFound<ForecastProviderSettings>();

        var tasks = providers
            .Select(x => _clientFactory
                .CreateClient(x.Name)
                .GetAsync(x.Url, cancellationToken)
                .ContinueWith(response => (httpResponse: response.Result, provider: x), TaskContinuationOptions.OnlyOnRanToCompletion))
            .ToList();

        var tuple = Array.Empty<(HttpResponseMessage message, ForecastProviderSettings provider)>();

        try
        {
            tuple = await Task.WhenAll(tasks);
        }
        catch
        {
            // ignored
        }

        if (!tuple.Any()) return Error.Unhandled($"All {providers.Count} providers failed to request forecast");

        var weathersTasks = tuple.Select(x => x.message.Content.ReadAsStringAsync(cancellationToken).ContinueWith(task =>
            new WeatherResponse(
                x.provider.Name,
                _jsonParserService.GetValueByPath<float>(task.Result, x.provider.TemperaturePath),
                _templateService.BuildTemplateFromJson(x.provider.ForecastTemplatePath, task.Result),
                _dateTimeService.UtcNow), cancellationToken));

        var weathers = await Task.WhenAll(weathersTasks);

        return new ListOf<WeatherResponse>(weathers);
    }
}