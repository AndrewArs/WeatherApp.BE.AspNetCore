using System.Collections;

namespace IntegrationTests.Data;

public class HttpClientsData : IEnumerable<object[]>
{
    private readonly Data[] _testData = {
        new()
        {
            Name = "WeatherApi",
            Slug = "weather-api",
            Url = "http://api.weatherapi.com/v1/current.json?key=secret_key&q=Lisbon&days=1",
            KeyParamName = "key",
            ExpectedUrl = "http://api.weatherapi.com/v1/current.json?key={secret}&q=Lisbon&days=1",
            Json = @"{
                ""location"": {
                  ""name"": ""Lisbon"",
                  ""region"": ""Lisboa"",
                  ""country"": ""Portugal"",
                  ""lat"": 38.72,
                  ""lon"": -9.13,
                  ""tz_id"": ""Europe/Lisbon"",
                  ""localtime_epoch"": 1687445099,
                  ""localtime"": ""2023-06-22 15:44""
                },
                ""current"": {
                  ""last_updated_epoch"": 1687444200,
                  ""last_updated"": ""2023-06-22 15:30"",
                  ""temp_c"": 27,
                  ""temp_f"": 80.6,
                  ""is_day"": 1,
                  ""condition"": {
                    ""text"": ""Partly cloudy"",
                    ""icon"": ""//cdn.weatherapi.com/weather/64x64/day/116.png"",
                    ""code"": 1003
                  },
                  ""wind_mph"": 13.6,
                  ""wind_kph"": 22,
                  ""wind_degree"": 320,
                  ""wind_dir"": ""NW"",
                  ""pressure_mb"": 1020,
                  ""pressure_in"": 30.12,
                  ""precip_mm"": 0,
                  ""precip_in"": 0,
                  ""humidity"": 48,
                  ""cloud"": 25,
                  ""feelslike_c"": 30.5,
                  ""feelslike_f"": 80.5,
                  ""vis_km"": 10,
                  ""vis_miles"": 6,
                  ""uv"": 7,
                  ""gust_mph"": 18.3,
                  ""gust_kph"": 29.5
                }
            }",
            Template = @"Weather from the source Weatherapi for location {{location.country}} {{location.name}}.
            Temperature is {{current.temp_c}} C (feels like {{current.feelslike_c}} C).
            {{current.condition.text}}. Wind speed {{current.wind_kph}} kilometers per hour.",
            ExpectedTemplate = @"Weather from the source Weatherapi for location Portugal Lisbon.
            Temperature is 27 C (feels like 30.5 C).
            Partly cloudy. Wind speed 22 kilometers per hour.",
            TemperaturePath = "current.temp_c",
            ExpectedTemperature = 27f
        },
        new()
        {
            Name = "crème brûlée in Paris.io 2",
            Slug = "creme-brulee-in-paris-io2",
            Url = "http://api.weather-forecast-provider.com?apikey=apikey",
            KeyParamName = "apikey",
            ExpectedUrl = "http://api.weather-forecast-provider.com?apikey={secret}",
            Json = @"{
              ""location"": {
                ""name"": ""Lisbon"",
                ""region"": ""Lisboa"",
                ""country"": ""Portugal""
              },
              ""data"": [
                {
                  ""temperature"": 20,
                  ""another_temperature"": 22
                },
                {
                  ""weather_conditions"": {
                    ""conditions"": [
                      ""hot"",
                      ""good for swimming""
                    ],
                    ""rain_conditions"": ""No rain expected""
                  }
                }
              ]
            }",
            Template = @"Weather forecast for location {{location.country}} {{location.region}}.
            Temperature: {{data.[0].temperature}} C. Conditions: {{data.[1].weather_conditions.conditions.[0]}},
            {{data.[1].weather_conditions.conditions.[1]}}, {{data.[1].weather_conditions.rain_conditions}}",
            ExpectedTemplate = @"Weather forecast for location Portugal Lisboa.
            Temperature: 20 C. Conditions: hot,
            good for swimming, No rain expected",
            TemperaturePath = "data.[0].temperature",
            ExpectedTemperature = 20f
        }
    };

    public IEnumerator<object[]> GetEnumerator()
    {
        foreach (var data in _testData)
        {

            yield return new object[]
            {
                data
            };
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public class Data
    {
        public required string Name { get; set; }
        public required string Slug { get; set; }
        public required string Url { get; set; }
        public required string Json { get; set; }
        public required string Template { get; set; }
        public required string TemperaturePath { get; set; }
        public string? KeyParamName { get; set; }
        public required string ExpectedTemplate { get; set; }
        public required string ExpectedUrl { get; set; }
        public required float ExpectedTemperature { get; set; }
    }
}