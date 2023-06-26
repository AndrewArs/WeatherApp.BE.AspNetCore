using Application.Mediatr.ForecastProvider.Shared;
using Domain.Entities;

namespace Application.Mediatr.ForecastProvider.Commands.Update;

public class UpdateForecastProviderHandler : IRequestResultHandler<UpdateForecastProviderCommand, ForecastProviderResponse>
{
    private readonly IDatabaseContext _databaseContext;

    public UpdateForecastProviderHandler(IDatabaseContext databaseContext)
    {
        _databaseContext = databaseContext;
    }

    public async Task<Result<ForecastProviderResponse>> Handle(UpdateForecastProviderCommand request, CancellationToken cancellationToken)
    {
        var entity = await _databaseContext.ForecastProviderSettings
            .FindAsync(request.Id, cancellationToken);

        if (entity is null) return Error.NotFound<ForecastProviderSettings>();

        if (request.Url is not null)
        {
            entity.Url = request.Url;
        }

        if (request.ForecastTemplatePath is not null)
        {
            entity.ForecastTemplatePath = request.ForecastTemplatePath;
        }

        if (request.KeyQueryParamName is not null)
        {
            entity.KeyQueryParamName = request.KeyQueryParamName;
        }

        if (request.Name is not null)
        {
            entity.Name = request.Name;
        }

        if (request.TemperaturePath is not null)
        {
            entity.TemperaturePath = request.TemperaturePath;
        }

        await _databaseContext.SaveChangesAsync(cancellationToken);

        return new ForecastProviderResponse(
            entity.Id,
            entity.CreatedAt,
            entity.UpdatedAt,
            entity.Name,
            entity.Url,
            entity.TemperaturePath,
            entity.ForecastTemplatePath,
            entity.KeyQueryParamName);
    }
}