using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Mediatr.ForecastProvider.Commands.Delete;

public class DeleteForecastProviderHandler : IRequestResultHandler<DeleteForecastProviderCommand>
{
    private readonly IDatabaseContext _databaseContext;

    public DeleteForecastProviderHandler(IDatabaseContext databaseContext)
    {
        _databaseContext = databaseContext;
    }

    public async Task<Result<Empty>> Handle(DeleteForecastProviderCommand request, CancellationToken cancellationToken)
    {
        var affectedRows = await _databaseContext.ForecastProviderSettings
            .Where(x => x.Id == request.Id)
            .ExecuteDeleteAsync(cancellationToken);

        if (affectedRows == 0) return Error.NotFound<ForecastProviderSettings>();

        return Result.Empty();
    }
}