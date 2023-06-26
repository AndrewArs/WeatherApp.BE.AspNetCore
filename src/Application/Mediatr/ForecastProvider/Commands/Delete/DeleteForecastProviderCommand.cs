namespace Application.Mediatr.ForecastProvider.Commands.Delete;

public class DeleteForecastProviderCommand : IRequestResult
{
    public required Guid Id { get; set; }
}