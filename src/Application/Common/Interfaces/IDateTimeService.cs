namespace Application.Common.Interfaces;

public interface IDateTimeService
{
    public DateTimeOffset UtcNow { get; }
}