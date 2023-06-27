using System.Net.Mime;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.Common;

[Route("api/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[ApiController]
public class ApiControllerBase : ControllerBase
{
    private ISender? _mediator;

    protected ISender Mediator => _mediator ??= HttpContext.RequestServices.GetRequiredService<ISender>();

    protected IActionResult HandleError(Error error)
        => error.Code switch
        {
            ErrorCodes.NotFound => NotFound(error.ToDto()),
            ErrorCodes.Forbidden => StatusCode(StatusCodes.Status403Forbidden, error.ToDto()),
            ErrorCodes.ValidationFailed => BadRequest(error.ToDto()),
            ErrorCodes.Unhandled => StatusCode(StatusCodes.Status500InternalServerError, error.ToDto()),
            _ => Problem()
        };

    protected CreatedAtActionResult CreatedAtAction(string? actionName, RouteValueDictionary routeValues)
    {
        return base.CreatedAtAction(actionName, routeValues, null);
    }
}
