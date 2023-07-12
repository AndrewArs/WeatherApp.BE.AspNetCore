using Infrastructure.Services;

namespace UnitTests.Tests;

public class UrlServiceTests
{
    public static IEnumerable<object[]> MaskUrlQueryParams_OneParamData()
    {
        yield return new object[]
        {
            "http://api.weatherapi.com/v1/current.json?key=some_secret&q=Lisbon&days=1&another_key=not_masked",
            "{secret}",
            "key",
            "http://api.weatherapi.com/v1/current.json?key={secret}&q=Lisbon&days=1&another_key=not_masked"
        };
        yield return new object[]
        {
            "https://weather.visualcrossing.com/VisualCrossingWebServices/rest/services/timeline/Lisbon?unitGroup=metric&include=days&product_key=some_secret_data",
            "mask",
            "product_key",
            "https://weather.visualcrossing.com/VisualCrossingWebServices/rest/services/timeline/Lisbon?unitGroup=metric&include=days&product_key=mask"
        };
        yield return new object[]
        {
            "api.openweathermap.org/data/2.5/weather?q=Lisbon&appid=myProductKey&",
            "[[place your token here]]",
            "appid",
            "api.openweathermap.org/data/2.5/weather?q=Lisbon&appid=[[place your token here]]&"
        };
        yield return new object[]
        {
            "http://some-url.com?product_key=my_key",
            "{{replace with your token}}",
            "product_key",
            "http://some-url.com?product_key={{replace with your token}}"
        };
    }

    [Theory]
    [MemberData(nameof(MaskUrlQueryParams_OneParamData))]
    public void MaskUrlQueryParams_OneParam(string url, string mask, string queryParam, string expected)
    {
        var urlService = new UrlService
        {
            Mask = mask
        };
        var result = urlService.MaskUrlQueryParams(url, queryParam);

        result.Should().BeEquivalentTo(expected);
    }

    public static IEnumerable<object[]> MaskUrlQueryParams_ManyParamsData()
    {
        yield return new object[]
        {
            "http://api.weatherapi.com/v1/current.json?key=some_secret&q=Lisbon&days=1",
            "{secret}",
            "http://api.weatherapi.com/v1/current.json?key={secret}&q={secret}&days={secret}",
            "key",
            "q",
            "days"
        };
        yield return new object[]
        {
            "https://weather.visualcrossing.com/VisualCrossingWebServices/rest/services/timeline/Lisbon?unitGroup=metric&include=days&product_key=some_secret_data",
            "mask",
            "https://weather.visualcrossing.com/VisualCrossingWebServices/rest/services/timeline/Lisbon?unitGroup=metric&include=mask&product_key=mask",
            "product_key",
            "include"
        };
    }

    [Theory]
    [MemberData(nameof(MaskUrlQueryParams_ManyParamsData))]
    public void MaskUrlQueryParams_ManyParams(string url, string mask, string expected, params string[] queryParams)
    {
        var urlService = new UrlService
        {
            Mask = mask
        };
        var result = urlService.MaskUrlQueryParams(url, queryParams);

        result.Should().BeEquivalentTo(expected);
    }
}