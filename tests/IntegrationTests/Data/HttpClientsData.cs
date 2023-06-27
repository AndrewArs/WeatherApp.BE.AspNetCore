using System.Collections;

namespace IntegrationTests.Data;

public class HttpClientsData : IEnumerable<object[]>
{
    public IEnumerator<object[]> GetEnumerator()
    {
        yield return new object[]
        {
            new Data
            {
                Name = "WeatherApi",
                Url = "http://api.weatherapi.com/v1/current.json?key=secret_key&q=Lisbon&days=1",
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
            ""temp_c"": 27.0,
            ""temp_f"": 80.6,
            ""is_day"": 1,
            ""condition"": {
            ""text"": ""Partly cloudy"",
            ""icon"": ""//cdn.weatherapi.com/weather/64x64/day/116.png"",
            ""code"": 1003
            },
            ""wind_mph"": 13.6,
            ""wind_kph"": 22.0,
            ""wind_degree"": 320,
            ""wind_dir"": ""NW"",
            ""pressure_mb"": 1020.0,
            ""pressure_in"": 30.12,
            ""precip_mm"": 0.0,
            ""precip_in"": 0.0,
            ""humidity"": 48,
            ""cloud"": 25,
            ""feelslike_c"": 30.5,
            ""feelslike_f"": 80.5,
            ""vis_km"": 10.0,
            ""vis_miles"": 6.0,
            ""uv"": 7.0,
            ""gust_mph"": 18.3,
            ""gust_kph"": 29.5
            }
            }",
                Template = @"Weather from the source Weatherapi for location {{location.country}} {{location.name}}.
            Temperature is {{current.temp_c}} C (feels like {{current.feelslike_c}} C).
            {{current.condition.text}}. Wind speed {{current.wind_kph}} kilometers per hour.",
                TemperaturePath = "current.temp_c",
                KeyParamName = "key",
                ExpectedTemplate = @"Weather from the source Weatherapi for location Portugal Lisbon.
            Temperature is 27.0 C (feels like 30.5 C).
            Partly cloudy. Wind speed 22.0 kilometers per hour.",
                ExpectedUrl = "http://api.weatherapi.com/v1/current.json?key={secret}&q=Lisbon&days=1",
                ExpectedTemperature = 27f
            }
        };
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public class Data
    {
        public required string Name { get; set; }
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