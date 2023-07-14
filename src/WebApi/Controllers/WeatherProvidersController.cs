using Application.Mediatr.ForecastProvider.Commands.Delete;
using Application.Mediatr.ForecastProvider.Queries.All;
using Application.Mediatr.ForecastProvider.Queries.One;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[Route("api/weather-providers")]
public class WeatherProvidersController : ApiControllerBase
{
    [HttpGet]
    [ProducesErrorResponseType(typeof(ErrorDto))]
    [ProducesResponseType(typeof(ListOfDto<ForecastProviderDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<IActionResult> GetAll()
    {
        var result = await Mediator.Send(new GetForecastProviderAllQuery());

        return result.Match(
            success => Ok(success.ToDto()),
            HandleError);
    }

    [HttpGet("{id-slug}")]
    [ProducesErrorResponseType(typeof(ErrorDto))]
    [ProducesResponseType(typeof(ForecastProviderDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<IActionResult> GetOne(
        [FromRoute(Name = "id-slug")] string idOrSlug,
        [FromQuery(Name = "hideUrlKey")] bool hideUrlKey = true)
    {
        var request = new GetForecastProviderQuery
        {
            HideUrlKey = hideUrlKey
        };
        if (Guid.TryParse(idOrSlug, out var guid))
        {
            request.Id = guid;
        }
        else
        {
            request.Slug = idOrSlug;
        }
        var result = await Mediator.Send(request);

        return result.Match(
            success => Ok(success.ToDto()),
            HandleError);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="dto">
    /// name                    : provider unique name 
    /// url                     : url which would be executed on each forecast request
    /// temperature_path        : json path to temperature value. example: data.[0].temperature_c - { "data": [ {"temperature_c": 20.0} ] }
    /// forecast_template_path  : template to be built on forecast request. should consist of text with json path placeholders {{json_path_placeholder}}. example: "Expected temperature for today is {{data.[0].temperature_c}} C"
    /// key_query_param_name    : param from query string to be masked with mask {secret}. example: http://api.weather.com?key=my_key -> http://api.weather.com?key={secret}
    /// </param>
    /// <returns></returns>
    [HttpPost]
    [ProducesErrorResponseType(typeof(ErrorDto))]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesDefaultResponseType]
    public async Task<IActionResult> Create([FromBody] CreateForecastProviderDto dto)
    {
        var result = await Mediator.Send(dto.ToDomain());

        return result.Match(
            success => CreatedAtAction(nameof(GetOne), new RouteValueDictionary { { "id-slug", success.Id.ToString("N") } }),
            HandleError);
    }

    [HttpPut("{id:guid}")]
    [ProducesErrorResponseType(typeof(ErrorDto))]
    [ProducesResponseType(typeof(ForecastProviderDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<IActionResult> UpdateById([FromRoute] Guid id, [FromBody] UpdateForecastProviderDto dto)
    {
        var result = await Mediator.Send(dto.ToDomain(id));

        return result.Match(
            success => Ok(success.ToDto()),
            HandleError);
    }

    [HttpDelete("{id:guid}")]
    [ProducesErrorResponseType(typeof(ErrorDto))]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<IActionResult> DeleteById([FromRoute] Guid id)
    {
        var result = await Mediator.Send(new DeleteForecastProviderCommand {Id = id});

        return result.Match(
            _ => NoContent(),
            HandleError);
    }
}
