namespace IntegrationTests.Infrastructure;

[Collection("Integration tests base collection")]
public abstract class BaseIntegrationTest : IAsyncLifetime
{
    private readonly IntegrationTestFactory _factory;
    protected HttpClient ApiClient => _factory.HttpClient;
    protected Uri ApiBaseAddress => _factory.Server.BaseAddress;

    protected BaseIntegrationTest(IntegrationTestFactory factory)
    {
        _factory = factory;
    }

    public Task InitializeAsync() => Task.CompletedTask;

    public Task DisposeAsync() => _factory.ResetDatabaseAsync();
}