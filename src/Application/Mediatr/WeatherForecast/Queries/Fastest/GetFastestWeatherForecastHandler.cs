using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Mediatr.WeatherForecast.Queries.Fastest;

public class GetFastestWeatherForecastHandler : IRequestResultHandler<GetFastestWeatherForecastQuery, WeatherForecastResponse>
{
    private readonly IHttpClientFactory _clientFactory;
    private readonly IDatabaseContext _databaseContext;
    private readonly ITemplateService _templateService;
    private readonly IJsonParserService _jsonParserService;
    private readonly IDateTimeService _dateTimeService;

    public GetFastestWeatherForecastHandler(
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

    public async Task<Result<WeatherForecastResponse>> Handle(GetFastestWeatherForecastQuery request, CancellationToken cancellationToken)
    {
        var providers = await _databaseContext.ForecastProviderSettings
            .AsNoTracking()
            .ToListAsync(cancellationToken);

        if (!providers.Any()) return Error.NotFound<ForecastProviderSettings>();

        using var cts = new CancellationTokenSource();
        using var ct = CancellationTokenSource.CreateLinkedTokenSource(cts.Token, cancellationToken);
        var tasks = providers
            .Select(x => _clientFactory
                .CreateClient(x.Slug)
                .GetAsync(x.Url, ct.Token)
                .ContinueWith(response => response.Result.EnsureSuccessStatusCode(), TaskContinuationOptions.OnlyOnRanToCompletion)
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

        return new WeatherForecastResponse(
            providerResponse.provider.Name,
            _jsonParserService.GetValueByPath<float>(json, providerResponse.provider.TemperaturePath),
            _templateService.BuildTemplateFromJson(providerResponse.provider.ForecastTemplatePath, json),
            _dateTimeService.UtcNow);
    }
}