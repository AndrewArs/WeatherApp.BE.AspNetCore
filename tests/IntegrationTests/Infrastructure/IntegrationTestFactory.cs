using System.Data.Common;
using FluentMigrator.Runner;
using Infrastructure;
using Infrastructure.Persistence;
using Infrastructure.Persistence.Migrations;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;
using Respawn;
using Respawn.Graph;
using Testcontainers.PostgreSql;
using WebApi;

namespace IntegrationTests.Infrastructure;

public class IntegrationTestFactory : WebApplicationFactory<Program>, IAsyncLifetime
{
    private readonly PostgreSqlContainer _postgreSqlContainer;
    private DbConnection _dbConnection = default!;
    private Respawner _respawner = default!;

    public HttpClient HttpClient { get; private set; } = default!;

    public IntegrationTestFactory()
    {
        _postgreSqlContainer = new PostgreSqlBuilder()
            .WithDatabase("weather-app")
            .WithImage("postgres:15-alpine")
            .WithPortBinding(64538, 5432)
            .WithCleanUp(true)
            .Build();
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureTestServices(services =>
        {
            services.RemoveDbContext<DatabaseContext>();
            services.AddDatabaseContext(_postgreSqlContainer.GetConnectionString());

            services.AddFluentMigratorCore()
                .ConfigureRunner(rb => rb
                    .AddPostgres()
                    .WithGlobalConnectionString(_postgreSqlContainer.GetConnectionString())
                    .ScanIn(typeof(AddForecastProviderSettings).Assembly).For.Migrations())
                .AddLogging(lb => lb.AddFluentMigratorConsole());

            services.AddHttpClients();

            services.RunDbMigrations();
        });
    }

    public Task ResetDatabaseAsync() => _respawner.ResetAsync(_dbConnection);

    public async Task InitializeAsync()
    {
        await _postgreSqlContainer.StartAsync();
        HttpClient = CreateClient();
        await InitializeRespawner();
    }

    public new Task DisposeAsync() => _postgreSqlContainer.DisposeAsync().AsTask();

    private async Task InitializeRespawner()
    {
        _dbConnection = new NpgsqlConnection(_postgreSqlContainer.GetConnectionString());
        await _dbConnection.OpenAsync();
        _respawner = await Respawner.CreateAsync(_dbConnection, new RespawnerOptions
        {
            DbAdapter = DbAdapter.Postgres,
            TablesToIgnore = new Table[] { "VersionInfo" }
        });
    }
}