namespace Domain.Common;

public abstract class BaseAuditableEntity : BaseIdentityEntity
{
    public DateTimeOffset UpdatedAt { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
}