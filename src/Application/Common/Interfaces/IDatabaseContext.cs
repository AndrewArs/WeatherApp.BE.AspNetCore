using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Common.Interfaces;

public interface IDatabaseContext
{
    public DbSet<ForecastProviderSettings> ForecastProviderSettings { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}