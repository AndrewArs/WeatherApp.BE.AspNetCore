using Domain.Common;

namespace Application.Common.Models;
public class EntityIdentifier
{
    public Guid Id { get; set; }

    public EntityIdentifier(Guid id)
    {
        Id = id;
    }

    public EntityIdentifier(BaseIdentityEntity identityEntity)
    {
        Id = identityEntity.Id;
    }
}
