using Application.Common.Interfaces;
using Infrastructure.Persistence;
using Infrastructure.Persistence.Interceptors;
using Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<AuditableEntityInterceptor>();
        services.AddDatabaseContext(configuration.GetConnectionString("Postgres"));

        services.AddScoped<IDateTimeService, DateTimeService>();
        services.AddSingleton<IJsonParserService, JsonParserService>();
        services.AddSingleton<ITemplateService, TemplateService>();
        services.AddSingleton<IUrlService, UrlService>();

        return services;
    }

    public static IServiceCollection AddDatabaseContext(this IServiceCollection services, string? connectionString)
    {
        services.AddDbContext<IDatabaseContext, DatabaseContext>(options => options
            .UseNpgsql(connectionString)
            .UseSnakeCaseNamingConvention());

        return services;
    }
}