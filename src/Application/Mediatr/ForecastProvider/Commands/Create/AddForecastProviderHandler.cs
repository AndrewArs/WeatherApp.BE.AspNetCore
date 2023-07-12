using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Mediatr.ForecastProvider.Commands.Create;

public class AddForecastProviderHandler : IRequestResultHandler<AddForecastProviderCommand, EntityIdentifier>
{
    private readonly IDatabaseContext _databaseContext;
    private readonly ISlugService _slugService;

    public AddForecastProviderHandler(IDatabaseContext databaseContext, ISlugService slugService)
    {
        _databaseContext = databaseContext;
        _slugService = slugService;
    }

    public async Task<Result<EntityIdentifier>> Handle(AddForecastProviderCommand request, CancellationToken cancellationToken)
    {
        var any = await _databaseContext.ForecastProviderSettings
            .AnyAsync(x => x.Name.ToLower().Equals(request.Name.ToLower()), cancellationToken);

        if (any) return Error.ValidationFailed("Name should be unique");

        var model = new ForecastProviderSettings
        {
            Url = request.Url,
            ForecastTemplatePath = request.ForecastTemplatePath,
            Name = request.Name,
            Slug = _slugService.Slugify(request.Name)!,
            TemperaturePath = request.TemperaturePath,
            KeyQueryParamName = request.KeyQueryParamName
        };

        _databaseContext.ForecastProviderSettings.Add(model);
        await _databaseContext.SaveChangesAsync(cancellationToken);

        return new EntityIdentifier(model);
    }
}