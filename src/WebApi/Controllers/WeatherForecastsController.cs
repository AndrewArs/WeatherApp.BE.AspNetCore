using Application.Mediatr.WeatherForecast.Queries.All;
using Application.Mediatr.WeatherForecast.Queries.Fastest;
using Application.Mediatr.WeatherForecast.Queries.One;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[Route("api/weather-providers/forecasts")]
public class WeatherForecastsController : ApiControllerBase
{
    [HttpGet]
    [ProducesErrorResponseType(typeof(ErrorDto))]
    [ProducesResponseType(typeof(ListOfDto<WeatherForecastDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<IActionResult> GetAll()
    {
        var result = await Mediator.Send(new GetWeatherForecastsQuery());

        return result.Match(
            success => Ok(success.ToDto()),
            HandleError);
    }

    [HttpGet("fastest")]
    [ProducesErrorResponseType(typeof(ErrorDto))]
    [ProducesResponseType(typeof(WeatherForecastDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<IActionResult> GetFastest()
    {
        var result = await Mediator.Send(new GetFastestWeatherForecastQuery());

        return result.Match(
            success => Ok(success.ToDto()),
            HandleError);
    }

    [HttpGet("~/api/weather-providers/{id-name}/forecasts")]
    [ProducesErrorResponseType(typeof(ErrorDto))]
    [ProducesResponseType(typeof(WeatherForecastDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<IActionResult> GetByProviderId([FromRoute(Name = "id-name")] string providerIdOrName)
    {
        var request = new GetWeatherForecastQuery();
        if (Guid.TryParse(providerIdOrName, out var id))
        {
            request.ProviderId = id;
        }
        else
        {
            request.ProviderName = providerIdOrName;
        }

        var result = await Mediator.Send(request);

        return result.Match(
            success => Ok(success.ToDto()),
            HandleError);
    }
}
