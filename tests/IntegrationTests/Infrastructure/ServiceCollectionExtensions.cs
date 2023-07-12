using System.Net.Mime;
using FluentMigrator.Runner;
using IntegrationTests.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RichardSzalay.MockHttp;

namespace IntegrationTests.Infrastructure;

public static class ServiceCollectionExtensions
{
    public static void RemoveDbContext<T>(this IServiceCollection services) where T : DbContext
    {
        var descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<T>));
        if (descriptor != null) services.Remove(descriptor);
    }

    public static void RunDbMigrations(this IServiceCollection services)
    {
        var serviceProvider = services.BuildServiceProvider();

        using var scope = serviceProvider.CreateScope();
        var runner = scope.ServiceProvider.GetRequiredService<IMigrationRunner>();
        runner.MigrateUp();
    }

    public static void AddHttpClients(this IServiceCollection services)
    {
        foreach (var data in new HttpClientsData().Select(x => x.First()).OfType<HttpClientsData.Data>())
        {
            var mockHandler = new MockHttpMessageHandler();
            mockHandler
                .When(data.Url)
                .Respond(MediaTypeNames.Application.Json, data.Json);

            services.AddHttpClient(data.Slug)
                .ConfigureHttpMessageHandlerBuilder(handlerBuilder => handlerBuilder.PrimaryHandler = mockHandler);
        }
    }
}