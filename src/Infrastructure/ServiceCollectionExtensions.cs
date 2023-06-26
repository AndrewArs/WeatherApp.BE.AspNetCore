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
        services.AddDbContext<IDatabaseContext, DatabaseContext>(options => options
            .UseNpgsql(configuration.GetConnectionString("Postgres"))
            .UseSnakeCaseNamingConvention());

        services.AddScoped<IDateTimeService, DateTimeService>();
        services.AddSingleton<IJsonParserService, JsonParserService>();
        services.AddSingleton<ITemplateService, TemplateService>();
        services.AddSingleton<IUrlService, UrlService>();

        return services;
    }
}