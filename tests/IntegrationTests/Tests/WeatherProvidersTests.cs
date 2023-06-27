using System.Net;
using System.Net.Http.Json;
using IntegrationTests.Data;
using IntegrationTests.Infrastructure;
using WebApi.Dtos;

namespace IntegrationTests.Tests;

public class WeatherProvidersTests : BaseIntegrationTest
{
    public WeatherProvidersTests(IntegrationTestFactory factory)
        : base(factory)
    {
    }

    [Theory]
    [ClassData(typeof(HttpClientsData))]
    public async Task AddProvider_Success(HttpClientsData.Data data)
    {
        var dto = new CreateForecastProviderDto(data.Name, data.Url, data.TemperaturePath, data.Template, data.KeyParamName);
        var response = await ApiClient.PostAsJsonAsync("api/weather-providers", dto);

        response.Should().BeSuccessful()
            .And.HaveStatusCode(HttpStatusCode.Created);

        response.Headers.Location!.ToString().Should().StartWith($"{ApiBaseAddress}api/weather-providers");

        var responseDto = await ApiClient.GetFromJsonAsync<WeatherProviderDto>(response.Headers.Location);

        responseDto.Should().NotBeNull();
        responseDto!.Url.Should().BeEquivalentTo(data.ExpectedUrl);
        responseDto.TemperaturePath.Should().BeEquivalentTo(data.TemperaturePath);
        responseDto.Name.Should().BeEquivalentTo(data.Name);
        responseDto.ForecastTemplatePath.Should().BeEquivalentTo(data.Template);
        responseDto.KeyQueryParamName.Should().BeEquivalentTo(data.KeyParamName);
        responseDto.CreatedAt.Should().BeCloseTo(DateTimeOffset.UtcNow, TimeSpan.FromSeconds(1));
        responseDto.UpdatedAt.Should().BeCloseTo(DateTimeOffset.UtcNow, TimeSpan.FromSeconds(1));

        var allDto = await ApiClient.GetFromJsonAsync<ListOfDto<WeatherProviderDto>>("api/weather-providers");

        allDto.Should().NotBeNull();
        allDto!.Data.Should().Contain(responseDto);
    }

    [Theory]
    [ClassData(typeof(HttpClientsData))]
    public async Task GetProvider_Success(HttpClientsData.Data data)
    {
        var dto = new CreateForecastProviderDto(data.Name, data.Url, data.TemperaturePath, data.Template, data.KeyParamName);
        var response = await ApiClient.PostAsJsonAsync("api/weather-providers", dto);

        var responseByIdDto = await ApiClient.GetFromJsonAsync<WeatherProviderDto>(response.Headers.Location);

        var responseByNameDto = await ApiClient.GetFromJsonAsync<WeatherProviderDto>($"api/weather-providers/{responseByIdDto!.Name}");

        responseByNameDto.Should().BeEquivalentTo(responseByIdDto);
    }

    [Theory]
    [ClassData(typeof(HttpClientsData))]
    public async Task DeleteProvider_Success(HttpClientsData.Data data)
    {
        var dto = new CreateForecastProviderDto(data.Name, data.Url, data.TemperaturePath, data.Template, data.KeyParamName);
        var response = await ApiClient.PostAsJsonAsync("api/weather-providers", dto);
        var resourceLocation = response.Headers.Location;

        var responseDto = await ApiClient.GetFromJsonAsync<WeatherProviderDto>(resourceLocation);

        response = await ApiClient.DeleteAsync($"api/weather-providers/{responseDto!.Id}");

        response.Should().BeSuccessful()
            .And.HaveStatusCode(HttpStatusCode.NoContent);

        response = await ApiClient.GetAsync(resourceLocation);

        response.Should().HaveStatusCode(HttpStatusCode.NotFound);
    }

    [Theory]
    [ClassData(typeof(HttpClientsData))]
    public async Task UpdateProvider_Success(HttpClientsData.Data data)
    {
        var dto = new CreateForecastProviderDto(data.Name, data.Url, data.TemperaturePath, data.Template, data.KeyParamName);
        
        var response = await ApiClient.PostAsJsonAsync("api/weather-providers", dto);
        var responseDto = await ApiClient.GetFromJsonAsync<WeatherProviderDto>(response.Headers.Location);
        
        var updateDto = new UpdateForecastProviderDto(
            data.Name + "v2",
            data.Url + "v2",
            data.TemperaturePath + "v2",
            data.Template + "v2",
            data.KeyParamName + "v2");
        response = await ApiClient.PutAsJsonAsync(response.Headers.Location, updateDto);

        response.Should().BeSuccessful()
            .And.HaveStatusCode(HttpStatusCode.OK);
        
        var updatedByIdDto = await response.Content.ReadFromJsonAsync<WeatherProviderDto>();

        updatedByIdDto.Should().NotBeNull();
        updatedByIdDto!.Url.Should().BeEquivalentTo(updateDto.Url);
        updatedByIdDto.TemperaturePath.Should().BeEquivalentTo(updateDto.TemperaturePath);
        updatedByIdDto.ForecastTemplatePath.Should().BeEquivalentTo(updateDto.ForecastTemplatePath);
        updatedByIdDto.KeyQueryParamName.Should().BeEquivalentTo(updateDto.KeyQueryParamName);
        updatedByIdDto.Id.Should().Be(responseDto!.Id);
        updatedByIdDto.Name.Should().BeEquivalentTo(updateDto.Name);
        updatedByIdDto.CreatedAt.Should().BeExactly(responseDto.CreatedAt);
        updatedByIdDto.UpdatedAt.Should().BeCloseTo(DateTimeOffset.UtcNow, TimeSpan.FromSeconds(1));
    }
}