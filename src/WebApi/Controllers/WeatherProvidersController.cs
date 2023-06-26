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
    [ProducesResponseType(typeof(ListOfDto<WeatherProviderDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<IActionResult> GetAll()
    {
        var result = await Mediator.Send(new GetForecastProviderAllQuery());

        return result.Match(
            success => Ok(success.ToDto()),
            HandleError);
    }

    [HttpGet("{id}")]
    [ProducesErrorResponseType(typeof(ErrorDto))]
    [ProducesResponseType(typeof(WeatherProviderDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<IActionResult> GetOne([FromRoute] string id)
    {
        var request = new GetForecastProviderQuery();
        if (Guid.TryParse(id, out var guid))
        {
            request.Id = guid;
        }
        else
        {
            request.Name = id;
        }
        var result = await Mediator.Send(request);

        return result.Match(
            success => Ok(success.ToDto()),
            HandleError);
    }

    [HttpPost]
    [ProducesErrorResponseType(typeof(ErrorDto))]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesDefaultResponseType]
    public async Task<IActionResult> Create([FromBody] CreateForecastProviderDto dto)
    {
        var result = await Mediator.Send(dto.ToDomain());

        return result.Match(
            success => CreatedAtAction(nameof(GetOne), new { id = success.Id.ToString("N") }, null),
            HandleError);
    }

    [HttpPut("{id:guid}")]
    [ProducesErrorResponseType(typeof(ErrorDto))]
    [ProducesResponseType(typeof(WeatherProviderDto), StatusCodes.Status200OK)]
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
