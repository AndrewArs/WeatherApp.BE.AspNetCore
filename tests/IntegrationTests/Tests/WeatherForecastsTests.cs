using IntegrationTests.Data;
using IntegrationTests.Infrastructure;
using System.Net.Http.Json;
using WebApi.Dtos;

namespace IntegrationTests.Tests;

public class WeatherForecastsTests : BaseIntegrationTest
{
    public WeatherForecastsTests(IntegrationTestFactory factory)
        : base(factory)
    {
    }

    [Theory]
    [ClassData(typeof(HttpClientsData))]
    public async Task GetForecasts_ByIdOrSlug_Success(HttpClientsData.Data data)
    {
        var dto = new CreateForecastProviderDto(data.Name, data.Url, data.TemperaturePath, data.Template, data.KeyParamName);
        var response = await ApiClient.PostAsJsonAsync("api/weather-providers", dto);
        var responseDto = await ApiClient.GetFromJsonAsync<ForecastProviderDto>(response.Headers.Location);

        var dtoById = await ApiClient.GetFromJsonAsync<WeatherForecastDto>($"/api/weather-providers/{responseDto!.Id}/forecasts");
        var dtoBySlug = await ApiClient.GetFromJsonAsync<WeatherForecastDto>($"/api/weather-providers/{responseDto.Slug}/forecasts");

        dtoById.Should().NotBeNull()
            .And.BeEquivalentTo(dtoBySlug, options => options.Excluding(x => x!.UpdatedAt));

        dtoById!.Provider.Should().BeEquivalentTo(data.Name);
        dtoById.Temperature.Should().Be(data.ExpectedTemperature);
        dtoById.UpdatedAt.Should().BeCloseTo(DateTimeOffset.UtcNow, TimeSpan.FromSeconds(1));
        dtoById.Weather.Should().Be(data.ExpectedTemplate);
    }

    [Theory]
    [ClassData(typeof(HttpClientsData))]
    public async Task GetForecasts_Fastest_Success(HttpClientsData.Data data)
    {
        var dto = new CreateForecastProviderDto(data.Name, data.Url, data.TemperaturePath, data.Template, data.KeyParamName);
        await ApiClient.PostAsJsonAsync("api/weather-providers", dto);

        var responseDto = await ApiClient.GetFromJsonAsync<WeatherForecastDto>("/api/weather-providers/forecasts/fastest");

        responseDto.Should().NotBeNull();
        responseDto!.Provider.Should().BeEquivalentTo(data.Name);
        responseDto.Temperature.Should().Be(data.ExpectedTemperature);
        responseDto.Weather.Should().Be(data.ExpectedTemplate);
        responseDto.UpdatedAt.Should().BeCloseTo(DateTimeOffset.UtcNow, TimeSpan.FromSeconds(1));
    }

    [Theory]
    [ClassData(typeof(HttpClientsData))]
    public async Task GetForecasts_All_Success(HttpClientsData.Data data)
    {
        var dto = new CreateForecastProviderDto(data.Name, data.Url, data.TemperaturePath, data.Template, data.KeyParamName);
        await ApiClient.PostAsJsonAsync("api/weather-providers", dto);

        var responseDto = await ApiClient.GetFromJsonAsync<ListOfDto<WeatherForecastDto>>("/api/weather-providers/forecasts");

        responseDto.Should().NotBeNull();
        responseDto!.Data.Should().Contain(x => x.Provider.Equals(data.Name) 
                                                && Math.Abs(x.Temperature - data.ExpectedTemperature) < 0.5f
                                                && x.Weather.Equals(data.ExpectedTemplate));
    }
}