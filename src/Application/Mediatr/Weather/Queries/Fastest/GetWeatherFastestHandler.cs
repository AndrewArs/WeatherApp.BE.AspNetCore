using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Mediatr.Weather.Queries.Fastest;

public class GetWeatherFastestHandler : IRequestResultHandler<GetWeatherFastestQuery, WeatherResponse>
{
    private readonly IHttpClientFactory _clientFactory;
    private readonly IDatabaseContext _databaseContext;
    private readonly ITemplateService _templateService;
    private readonly IJsonParserService _jsonParserService;
    private readonly IDateTimeService _dateTimeService;

    public GetWeatherFastestHandler(
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

    public async Task<Result<WeatherResponse>> Handle(GetWeatherFastestQuery request, CancellationToken cancellationToken)
    {
        var providers = await _databaseContext.ForecastProviderSettings
            .AsNoTracking()
            .ToListAsync(cancellationToken);

        if (!providers.Any()) return Error.NotFound<ForecastProviderSettings>();

        using var cts = new CancellationTokenSource();
        using var ct = CancellationTokenSource.CreateLinkedTokenSource(cts.Token, cancellationToken);
        var tasks = providers
            .Select(x => _clientFactory
                .CreateClient(x.Name)
                .GetAsync(x.Url, ct.Token)
                .ContinueWith(response => new { httpResponse = response.Result, provider = x}, TaskContinuationOptions.OnlyOnRanToCompletion))
            .ToList();
        var task = tasks.First();

        while (task.Status != TaskStatus.RanToCompletion && tasks.Any())
        {
            task = await Task.WhenAny(tasks);

            if (task.Status != TaskStatus.RanToCompletion)
            {
                tasks.Remove(task);
            }
        }

        var providerResponse = await task;
        cts.Cancel();

        var json = await providerResponse.httpResponse.Content.ReadAsStringAsync(cancellationToken);

        return new WeatherResponse(
            providerResponse.provider.Name,
            _jsonParserService.GetValueByPath<int>(json, providerResponse.provider.TemperaturePath),
            _templateService.BuildTemplateFromJson(providerResponse.provider.ForecastTemplatePath, json),
            _dateTimeService.UtcNow);
    }
}