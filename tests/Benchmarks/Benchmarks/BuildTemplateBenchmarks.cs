using BenchmarkDotNet.Attributes;
using Infrastructure.Services;

namespace Benchmarks.Benchmarks;

public class BuildTemplateBenchmarks
{
    private readonly TemplateService _templateService;
    private readonly JsonParserService _jsonParserService;

    public BuildTemplateBenchmarks()
    {
        _jsonParserService = new JsonParserService();
        _templateService = new TemplateService(_jsonParserService);
    }

    public static IEnumerable<object[]> BuildTemplateFromJsonData
        => new List<object[]>
    {
            new object[]
            {
                $"{Guid.NewGuid()}. Weather from the source Weatherapi for location {{location.country}} {{location.name}}. Temperature is {{current.temp_c}} C (feels like {{current.feelslike_c}} C). {{current.condition.text}}. Wind speed {{current.wind_kph}} kilometers per hour.",
                @"{
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
                    ""feelslike_c"": 27.0,
                    ""feelslike_f"": 80.5,
                    ""vis_km"": 10.0,
                    ""vis_miles"": 6.0,
                    ""uv"": 7.0,
                    ""gust_mph"": 18.3,
                    ""gust_kph"": 29.5
                    }
                }"
            },
            new object[]
            {
                $"{Guid.NewGuid()}. Weather from the source Visualcrossing for location {{resolvedAddress}}. Temperature is {{days.[0].temp}} C (feels like {{days.[0].feelslike}} C). {{days.[0].description}} Wind speed {{days.[0].windspeed}}.",
                @"{
                    ""queryCost"": 1,
                    ""latitude"": 38.7264,
                    ""longitude"": -9.14949,
                    ""resolvedAddress"": ""Lisboa, Portugal"",
                    ""address"": ""Lisbon"",
                    ""timezone"": ""Europe/Lisbon"",
                    ""tzoffset"": 1.0,
                    ""days"": [
                    {
                    ""datetime"": ""2023-06-22"",
                    ""datetimeEpoch"": 1687388400,
                    ""tempmax"": 27.3,
                    ""tempmin"": 17.0,
                    ""temp"": 21.8,
                    ""feelslikemax"": 27.8,
                    ""feelslikemin"": 17.0,
                    ""feelslike"": 21.8,
                    ""dew"": 15.1,
                    ""humidity"": 67.9,
                    ""precip"": 0.0,
                    ""precipprob"": 0.0,
                    ""precipcover"": 0.0,
                    ""preciptype"": null,
                    ""snow"": 0.0,
                    ""snowdepth"": 0.0,
                    ""windgust"": 31.7,
                    ""windspeed"": 20.5,
                    ""winddir"": 346.5,
                    ""pressure"": 1019.9,
                    ""cloudcover"": 18.5,
                    ""visibility"": 14.7,
                    ""solarradiation"": 345.4,
                    ""solarenergy"": 29.8,
                    ""uvindex"": 9.0,
                    ""severerisk"": 10.0,
                    ""sunrise"": ""06:12:16"",
                    ""sunriseEpoch"": 1687410736,
                    ""sunset"": ""21:04:58"",
                    ""sunsetEpoch"": 1687464298,
                    ""moonphase"": 0.14,
                    ""conditions"": ""Clear"",
                    ""description"": ""Clear conditions throughout the day."",
                    ""icon"": ""clear-day"",
                    ""stations"": [
                    ""D7126"",
                    ""LPST"",
                    ""LPAR"",
                    ""LPMT"",
                    ""LPPT""
                    ],
                    ""source"": ""comb""
                    },
                    {
                    ""datetime"": ""2023-06-23"",
                    ""datetimeEpoch"": 1687474800,
                    ""tempmax"": 33.1,
                    ""tempmin"": 19.5,
                    ""temp"": 25.5,
                    ""feelslikemax"": 34.0,
                    ""feelslikemin"": 19.5,
                    ""feelslike"": 25.6,
                    ""dew"": 16.1,
                    ""humidity"": 58.5,
                    ""precip"": 0.0,
                    ""precipprob"": 0.0,
                    ""precipcover"": 0.0,
                    ""preciptype"": null,
                    ""snow"": 0.0,
                    ""snowdepth"": 0.0,
                    ""windgust"": 25.9,
                    ""windspeed"": 14.8,
                    ""winddir"": 355.8,
                    ""pressure"": 1019.3,
                    ""cloudcover"": 0.1,
                    ""visibility"": 24.1,
                    ""solarradiation"": 356.3,
                    ""solarenergy"": 30.9,
                    ""uvindex"": 10.0,
                    ""severerisk"": 10.0,
                    ""sunrise"": ""06:12:31"",
                    ""sunriseEpoch"": 1687497151,
                    ""sunset"": ""21:05:08"",
                    ""sunsetEpoch"": 1687550708,
                    ""moonphase"": 0.17,
                    ""conditions"": ""Clear"",
                    ""description"": ""Clear conditions throughout the day."",
                    ""icon"": ""clear-day"",
                    ""stations"": null,
                    ""source"": ""fcst""
                    },
                    {
                    ""datetime"": ""2023-06-24"",
                    ""datetimeEpoch"": 1687561200,
                    ""tempmax"": 33.9,
                    ""tempmin"": 21.7,
                    ""temp"": 27.1,
                    ""feelslikemax"": 36.1,
                    ""feelslikemin"": 21.7,
                    ""feelslike"": 27.4,
                    ""dew"": 17.1,
                    ""humidity"": 55.8,
                    ""precip"": 0.0,
                    ""precipprob"": 0.0,
                    ""precipcover"": 0.0,
                    ""preciptype"": null,
                    ""snow"": 0.0,
                    ""snowdepth"": 0.0,
                    ""windgust"": 27.0,
                    ""windspeed"": 15.8,
                    ""winddir"": 327.1,
                    ""pressure"": 1015.5,
                    ""cloudcover"": 0.0,
                    ""visibility"": 24.1,
                    ""solarradiation"": 352.5,
                    ""solarenergy"": 30.6,
                    ""uvindex"": 10.0,
                    ""severerisk"": 10.0,
                    ""sunrise"": ""06:12:47"",
                    ""sunriseEpoch"": 1687583567,
                    ""sunset"": ""21:05:16"",
                    ""sunsetEpoch"": 1687637116,
                    ""moonphase"": 0.2,
                    ""conditions"": ""Clear"",
                    ""description"": ""Clear conditions throughout the day."",
                    ""icon"": ""clear-day"",
                    ""stations"": null,
                    ""source"": ""fcst""
                    },
                    {
                    ""datetime"": ""2023-06-25"",
                    ""datetimeEpoch"": 1687647600,
                    ""tempmax"": 33.0,
                    ""tempmin"": 21.8,
                    ""temp"": 26.4,
                    ""feelslikemax"": 32.3,
                    ""feelslikemin"": 21.8,
                    ""feelslike"": 26.3,
                    ""dew"": 15.5,
                    ""humidity"": 52.9,
                    ""precip"": 0.0,
                    ""precipprob"": 0.0,
                    ""precipcover"": 0.0,
                    ""preciptype"": null,
                    ""snow"": 0.0,
                    ""snowdepth"": 0.0,
                    ""windgust"": 31.7,
                    ""windspeed"": 18.4,
                    ""winddir"": 353.4,
                    ""pressure"": 1016.1,
                    ""cloudcover"": 0.4,
                    ""visibility"": 24.1,
                    ""solarradiation"": 356.1,
                    ""solarenergy"": 30.9,
                    ""uvindex"": 10.0,
                    ""severerisk"": 10.0,
                    ""sunrise"": ""06:13:05"",
                    ""sunriseEpoch"": 1687669985,
                    ""sunset"": ""21:05:23"",
                    ""sunsetEpoch"": 1687723523,
                    ""moonphase"": 0.24,
                    ""conditions"": ""Clear"",
                    ""description"": ""Clear conditions throughout the day."",
                    ""icon"": ""clear-day"",
                    ""stations"": null,
                    ""source"": ""fcst""
                    },
                    {
                    ""datetime"": ""2023-06-26"",
                    ""datetimeEpoch"": 1687734000,
                    ""tempmax"": 35.3,
                    ""tempmin"": 22.0,
                    ""temp"": 27.2,
                    ""feelslikemax"": 34.4,
                    ""feelslikemin"": 22.0,
                    ""feelslike"": 27.0,
                    ""dew"": 15.2,
                    ""humidity"": 50.5,
                    ""precip"": 0.0,
                    ""precipprob"": 0.0,
                    ""precipcover"": 0.0,
                    ""preciptype"": null,
                    ""snow"": 0.0,
                    ""snowdepth"": 0.0,
                    ""windgust"": 28.8,
                    ""windspeed"": 17.6,
                    ""winddir"": 4.1,
                    ""pressure"": 1016.6,
                    ""cloudcover"": 4.9,
                    ""visibility"": 24.1,
                    ""solarradiation"": 360.7,
                    ""solarenergy"": 31.1,
                    ""uvindex"": 10.0,
                    ""severerisk"": 10.0,
                    ""sunrise"": ""06:13:25"",
                    ""sunriseEpoch"": 1687756405,
                    ""sunset"": ""21:05:27"",
                    ""sunsetEpoch"": 1687809927,
                    ""moonphase"": 0.25,
                    ""conditions"": ""Clear"",
                    ""description"": ""Clear conditions throughout the day."",
                    ""icon"": ""clear-day"",
                    ""stations"": null,
                    ""source"": ""fcst""
                    },
                    {
                    ""datetime"": ""2023-06-27"",
                    ""datetimeEpoch"": 1687820400,
                    ""tempmax"": 34.2,
                    ""tempmin"": 20.1,
                    ""temp"": 26.6,
                    ""feelslikemax"": 32.7,
                    ""feelslikemin"": 20.1,
                    ""feelslike"": 26.1,
                    ""dew"": 14.0,
                    ""humidity"": 49.5,
                    ""precip"": 0.0,
                    ""precipprob"": 0.0,
                    ""precipcover"": 0.0,
                    ""preciptype"": null,
                    ""snow"": 0.0,
                    ""snowdepth"": 0.0,
                    ""windgust"": 38.2,
                    ""windspeed"": 21.2,
                    ""winddir"": 339.3,
                    ""pressure"": 1016.5,
                    ""cloudcover"": 1.9,
                    ""visibility"": 23.4,
                    ""solarradiation"": 363.2,
                    ""solarenergy"": 31.3,
                    ""uvindex"": 10.0,
                    ""severerisk"": 10.0,
                    ""sunrise"": ""06:13:46"",
                    ""sunriseEpoch"": 1687842826,
                    ""sunset"": ""21:05:30"",
                    ""sunsetEpoch"": 1687896330,
                    ""moonphase"": 0.3,
                    ""conditions"": ""Clear"",
                    ""description"": ""Clear conditions throughout the day."",
                    ""icon"": ""clear-day"",
                    ""stations"": null,
                    ""source"": ""fcst""
                    },
                    {
                    ""datetime"": ""2023-06-28"",
                    ""datetimeEpoch"": 1687906800,
                    ""tempmax"": 29.1,
                    ""tempmin"": 19.5,
                    ""temp"": 23.5,
                    ""feelslikemax"": 28.9,
                    ""feelslikemin"": 19.5,
                    ""feelslike"": 23.5,
                    ""dew"": 15.8,
                    ""humidity"": 64.1,
                    ""precip"": 0.0,
                    ""precipprob"": 0.0,
                    ""precipcover"": 0.0,
                    ""preciptype"": null,
                    ""snow"": 0.0,
                    ""snowdepth"": 0.0,
                    ""windgust"": 65.5,
                    ""windspeed"": 33.8,
                    ""winddir"": 337.7,
                    ""pressure"": 1016.1,
                    ""cloudcover"": 4.0,
                    ""visibility"": 23.3,
                    ""solarradiation"": 356.0,
                    ""solarenergy"": 30.6,
                    ""uvindex"": 9.0,
                    ""severerisk"": 10.0,
                    ""sunrise"": ""06:14:09"",
                    ""sunriseEpoch"": 1687929249,
                    ""sunset"": ""21:05:31"",
                    ""sunsetEpoch"": 1687982731,
                    ""moonphase"": 0.34,
                    ""conditions"": ""Clear"",
                    ""description"": ""Clear conditions throughout the day."",
                    ""icon"": ""clear-day"",
                    ""stations"": null,
                    ""source"": ""fcst""
                    },
                    {
                    ""datetime"": ""2023-06-29"",
                    ""datetimeEpoch"": 1687993200,
                    ""tempmax"": 31.5,
                    ""tempmin"": 19.0,
                    ""temp"": 24.2,
                    ""feelslikemax"": 31.0,
                    ""feelslikemin"": 19.0,
                    ""feelslike"": 24.2,
                    ""dew"": 15.2,
                    ""humidity"": 59.1,
                    ""precip"": 0.0,
                    ""precipprob"": 0.0,
                    ""precipcover"": 0.0,
                    ""preciptype"": null,
                    ""snow"": 0.0,
                    ""snowdepth"": 0.0,
                    ""windgust"": 62.4,
                    ""windspeed"": 25.0,
                    ""winddir"": 339.8,
                    ""pressure"": 1014.6,
                    ""cloudcover"": 0.0,
                    ""visibility"": 24.1,
                    ""solarradiation"": 351.3,
                    ""solarenergy"": 30.2,
                    ""uvindex"": 9.0,
                    ""severerisk"": 10.0,
                    ""sunrise"": ""06:14:33"",
                    ""sunriseEpoch"": 1688015673,
                    ""sunset"": ""21:05:30"",
                    ""sunsetEpoch"": 1688069130,
                    ""moonphase"": 0.37,
                    ""conditions"": ""Clear"",
                    ""description"": ""Clear conditions throughout the day."",
                    ""icon"": ""clear-day"",
                    ""stations"": null,
                    ""source"": ""fcst""
                    },
                    {
                    ""datetime"": ""2023-06-30"",
                    ""datetimeEpoch"": 1688079600,
                    ""tempmax"": 33.1,
                    ""tempmin"": 22.3,
                    ""temp"": 26.9,
                    ""feelslikemax"": 31.9,
                    ""feelslikemin"": 22.3,
                    ""feelslike"": 26.6,
                    ""dew"": 15.1,
                    ""humidity"": 50.6,
                    ""precip"": 0.0,
                    ""precipprob"": 0.0,
                    ""precipcover"": 0.0,
                    ""preciptype"": null,
                    ""snow"": 0.0,
                    ""snowdepth"": 0.0,
                    ""windgust"": 20.2,
                    ""windspeed"": 16.2,
                    ""winddir"": 253.0,
                    ""pressure"": 1015.4,
                    ""cloudcover"": 0.0,
                    ""visibility"": 24.1,
                    ""solarradiation"": 353.1,
                    ""solarenergy"": 30.3,
                    ""uvindex"": 9.0,
                    ""severerisk"": 10.0,
                    ""sunrise"": ""06:14:59"",
                    ""sunriseEpoch"": 1688102099,
                    ""sunset"": ""21:05:27"",
                    ""sunsetEpoch"": 1688155527,
                    ""moonphase"": 0.4,
                    ""conditions"": ""Clear"",
                    ""description"": ""Clear conditions throughout the day."",
                    ""icon"": ""clear-day"",
                    ""stations"": null,
                    ""source"": ""fcst""
                    },
                    {
                    ""datetime"": ""2023-07-01"",
                    ""datetimeEpoch"": 1688166000,
                    ""tempmax"": 35.2,
                    ""tempmin"": 22.3,
                    ""temp"": 28.3,
                    ""feelslikemax"": 33.9,
                    ""feelslikemin"": 22.3,
                    ""feelslike"": 27.9,
                    ""dew"": 13.9,
                    ""humidity"": 43.5,
                    ""precip"": 0.0,
                    ""precipprob"": 0.0,
                    ""precipcover"": 0.0,
                    ""preciptype"": null,
                    ""snow"": 0.0,
                    ""snowdepth"": 0.0,
                    ""windgust"": 16.2,
                    ""windspeed"": 14.8,
                    ""winddir"": 273.6,
                    ""pressure"": 1015.6,
                    ""cloudcover"": 0.0,
                    ""visibility"": 24.1,
                    ""solarradiation"": 358.9,
                    ""solarenergy"": 31.2,
                    ""uvindex"": 10.0,
                    ""severerisk"": 10.0,
                    ""sunrise"": ""06:15:26"",
                    ""sunriseEpoch"": 1688188526,
                    ""sunset"": ""21:05:23"",
                    ""sunsetEpoch"": 1688241923,
                    ""moonphase"": 0.43,
                    ""conditions"": ""Clear"",
                    ""description"": ""Clear conditions throughout the day."",
                    ""icon"": ""clear-day"",
                    ""stations"": null,
                    ""source"": ""fcst""
                    },
                    {
                    ""datetime"": ""2023-07-02"",
                    ""datetimeEpoch"": 1688252400,
                    ""tempmax"": 37.1,
                    ""tempmin"": 23.7,
                    ""temp"": 30.0,
                    ""feelslikemax"": 35.2,
                    ""feelslikemin"": 23.7,
                    ""feelslike"": 29.1,
                    ""dew"": 11.7,
                    ""humidity"": 35.4,
                    ""precip"": 0.0,
                    ""precipprob"": 0.0,
                    ""precipcover"": 0.0,
                    ""preciptype"": null,
                    ""snow"": 0.0,
                    ""snowdepth"": 0.0,
                    ""windgust"": 21.2,
                    ""windspeed"": 14.8,
                    ""winddir"": 303.7,
                    ""pressure"": 1014.9,
                    ""cloudcover"": 5.1,
                    ""visibility"": 24.1,
                    ""solarradiation"": 353.6,
                    ""solarenergy"": 30.6,
                    ""uvindex"": 9.0,
                    ""severerisk"": 10.0,
                    ""sunrise"": ""06:15:54"",
                    ""sunriseEpoch"": 1688274954,
                    ""sunset"": ""21:05:16"",
                    ""sunsetEpoch"": 1688328316,
                    ""moonphase"": 0.47,
                    ""conditions"": ""Clear"",
                    ""description"": ""Clear conditions throughout the day."",
                    ""icon"": ""clear-day"",
                    ""stations"": null,
                    ""source"": ""fcst""
                    },
                    {
                    ""datetime"": ""2023-07-03"",
                    ""datetimeEpoch"": 1688338800,
                    ""tempmax"": 37.3,
                    ""tempmin"": 25.6,
                    ""temp"": 31.2,
                    ""feelslikemax"": 35.0,
                    ""feelslikemin"": 25.6,
                    ""feelslike"": 29.9,
                    ""dew"": 8.8,
                    ""humidity"": 26.8,
                    ""precip"": 0.0,
                    ""precipprob"": 4.8,
                    ""precipcover"": 0.0,
                    ""preciptype"": null,
                    ""snow"": 0.0,
                    ""snowdepth"": 0.0,
                    ""windgust"": 14.0,
                    ""windspeed"": 13.0,
                    ""winddir"": 274.3,
                    ""pressure"": 1015.1,
                    ""cloudcover"": 0.0,
                    ""visibility"": 24.1,
                    ""solarradiation"": 355.7,
                    ""solarenergy"": 30.6,
                    ""uvindex"": 10.0,
                    ""severerisk"": 10.0,
                    ""sunrise"": ""06:16:24"",
                    ""sunriseEpoch"": 1688361384,
                    ""sunset"": ""21:05:08"",
                    ""sunsetEpoch"": 1688414708,
                    ""moonphase"": 0.5,
                    ""conditions"": ""Clear"",
                    ""description"": ""Clear conditions throughout the day."",
                    ""icon"": ""clear-day"",
                    ""stations"": null,
                    ""source"": ""fcst""
                    },
                    {
                    ""datetime"": ""2023-07-04"",
                    ""datetimeEpoch"": 1688425200,
                    ""tempmax"": 35.1,
                    ""tempmin"": 25.6,
                    ""temp"": 30.1,
                    ""feelslikemax"": 33.2,
                    ""feelslikemin"": 25.6,
                    ""feelslike"": 29.0,
                    ""dew"": 9.8,
                    ""humidity"": 29.2,
                    ""precip"": 0.0,
                    ""precipprob"": 4.8,
                    ""precipcover"": 0.0,
                    ""preciptype"": null,
                    ""snow"": 0.0,
                    ""snowdepth"": 0.0,
                    ""windgust"": 24.5,
                    ""windspeed"": 18.0,
                    ""winddir"": 250.9,
                    ""pressure"": 1015.1,
                    ""cloudcover"": 0.6,
                    ""visibility"": 24.1,
                    ""solarradiation"": 355.9,
                    ""solarenergy"": 30.6,
                    ""uvindex"": 10.0,
                    ""severerisk"": 10.0,
                    ""sunrise"": ""06:16:55"",
                    ""sunriseEpoch"": 1688447815,
                    ""sunset"": ""21:04:58"",
                    ""sunsetEpoch"": 1688501098,
                    ""moonphase"": 0.53,
                    ""conditions"": ""Clear"",
                    ""description"": ""Clear conditions throughout the day."",
                    ""icon"": ""clear-day"",
                    ""stations"": null,
                    ""source"": ""fcst""
                    },
                    {
                    ""datetime"": ""2023-07-05"",
                    ""datetimeEpoch"": 1688511600,
                    ""tempmax"": 31.9,
                    ""tempmin"": 23.2,
                    ""temp"": 27.3,
                    ""feelslikemax"": 30.8,
                    ""feelslikemin"": 23.2,
                    ""feelslike"": 27.0,
                    ""dew"": 14.3,
                    ""humidity"": 46.5,
                    ""precip"": 0.0,
                    ""precipprob"": 0.0,
                    ""precipcover"": 0.0,
                    ""preciptype"": null,
                    ""snow"": 0.0,
                    ""snowdepth"": 0.0,
                    ""windgust"": 19.1,
                    ""windspeed"": 16.6,
                    ""winddir"": 234.5,
                    ""pressure"": 1013.6,
                    ""cloudcover"": 25.2,
                    ""visibility"": 24.1,
                    ""solarradiation"": 338.6,
                    ""solarenergy"": 29.3,
                    ""uvindex"": 9.0,
                    ""severerisk"": 10.0,
                    ""sunrise"": ""06:17:27"",
                    ""sunriseEpoch"": 1688534247,
                    ""sunset"": ""21:04:45"",
                    ""sunsetEpoch"": 1688587485,
                    ""moonphase"": 0.57,
                    ""conditions"": ""Partially cloudy"",
                    ""description"": ""Becoming cloudy in the afternoon."",
                    ""icon"": ""partly-cloudy-day"",
                    ""stations"": null,
                    ""source"": ""fcst""
                    },
                    {
                    ""datetime"": ""2023-07-06"",
                    ""datetimeEpoch"": 1688598000,
                    ""tempmax"": 26.8,
                    ""tempmin"": 19.7,
                    ""temp"": 22.8,
                    ""feelslikemax"": 27.2,
                    ""feelslikemin"": 19.7,
                    ""feelslike"": 22.8,
                    ""dew"": 16.1,
                    ""humidity"": 67.2,
                    ""precip"": 0.0,
                    ""precipprob"": 0.0,
                    ""precipcover"": 0.0,
                    ""preciptype"": null,
                    ""snow"": 0.0,
                    ""snowdepth"": 0.0,
                    ""windgust"": 35.3,
                    ""windspeed"": 23.8,
                    ""winddir"": 322.7,
                    ""pressure"": 1012.9,
                    ""cloudcover"": 73.3,
                    ""visibility"": 23.3,
                    ""solarradiation"": 290.5,
                    ""solarenergy"": 25.2,
                    ""uvindex"": 7.0,
                    ""severerisk"": 10.0,
                    ""sunrise"": ""06:18:01"",
                    ""sunriseEpoch"": 1688620681,
                    ""sunset"": ""21:04:31"",
                    ""sunsetEpoch"": 1688673871,
                    ""moonphase"": 0.6,
                    ""conditions"": ""Partially cloudy"",
                    ""description"": ""Partly cloudy throughout the day."",
                    ""icon"": ""partly-cloudy-day"",
                    ""stations"": null,
                    ""source"": ""fcst""
                    }
                    ],
                    ""stations"": {
                    ""LPMT"": {
                    ""distance"": 11625.0,
                    ""latitude"": 38.7,
                    ""longitude"": -9.02,
                    ""useCount"": 0,
                    ""id"": ""LPMT"",
                    ""name"": ""LPMT"",
                    ""quality"": 47,
                    ""contribution"": 0.0
                    },
                    ""LPPT"": {
                    ""distance"": 6202.0,
                    ""latitude"": 38.78,
                    ""longitude"": -9.13,
                    ""useCount"": 0,
                    ""id"": ""LPPT"",
                    ""name"": ""LPPT"",
                    ""quality"": 50,
                    ""contribution"": 0.0
                    },
                    ""D7126"": {
                    ""distance"": 57855.0,
                    ""latitude"": 39.231,
                    ""longitude"": -9.308,
                    ""useCount"": 0,
                    ""id"": ""D7126"",
                    ""name"": ""DW7126 Lourinha PT"",
                    ""quality"": 0,
                    ""contribution"": 0.0
                    },
                    ""LPST"": {
                    ""distance"": 18815.0,
                    ""latitude"": 38.82,
                    ""longitude"": -9.33,
                    ""useCount"": 0,
                    ""id"": ""LPST"",
                    ""name"": ""LPST"",
                    ""quality"": 14,
                    ""contribution"": 0.0
                    },
                    ""LPAR"": {
                    ""distance"": 20459.0,
                    ""latitude"": 38.88,
                    ""longitude"": -9.02,
                    ""useCount"": 0,
                    ""id"": ""LPAR"",
                    ""name"": ""LPAR"",
                    ""quality"": 13,
                    ""contribution"": 0.0
                    }
                    }
                }"
            },
            new object[]
            {
                $"{Guid.NewGuid()}. Weather from the source OpenWeatherMap for location {{sys.country}} {{name}}. Temperature is {{main.temp}} F (feels like {{main.feels_like}} F). {{weather.[0].description}}. Wind speed {{wind.speed}}.",
                @"{
                    ""coord"": {
                    ""lon"": -9.1333,
                    ""lat"": 38.7167
                    },
                    ""weather"": [
                    {
                    ""id"": 801,
                    ""main"": ""Clouds"",
                    ""description"": ""few clouds"",
                    ""icon"": ""02d""
                    }
                    ],
                    ""base"": ""stations"",
                    ""main"": {
                    ""temp"": 300.09,
                    ""feels_like"": 300.34,
                    ""temp_min"": 298.45,
                    ""temp_max"": 301.83,
                    ""pressure"": 1021,
                    ""humidity"": 47
                    },
                    ""visibility"": 10000,
                    ""wind"": {
                    ""speed"": 8.75,
                    ""deg"": 340
                    },
                    ""clouds"": {
                    ""all"": 20
                    },
                    ""dt"": 1687445731,
                    ""sys"": {
                    ""type"": 1,
                    ""id"": 6901,
                    ""country"": ""PT"",
                    ""sunrise"": 1687410726,
                    ""sunset"": 1687464284
                    },
                    ""timezone"": 3600,
                    ""id"": 2267057,
                    ""name"": ""Lisbon"",
                    ""cod"": 200
                }"
            },
            new object[]
            {
                $"{Guid.NewGuid()}. Weather from the source Tomorrow.io for location {{location.name}}. Temperature is {{data.values.temperature}} C (feels like {{data.values.temperatureApparent}} C). Wind speed {{data.values.windSpeed}} kilometers per hour.",
                @"{
                    ""data"": {
                    ""time"": ""2023-06-22T15:02:00Z"",
                    ""values"": {
                    ""cloudBase"": 1.71,
                    ""cloudCeiling"": null,
                    ""cloudCover"": 19,
                    ""dewPoint"": 14.5,
                    ""freezingRainIntensity"": 0,
                    ""humidity"": 44,
                    ""precipitationProbability"": 0,
                    ""pressureSurfaceLevel"": 1018.87,
                    ""rainIntensity"": 0,
                    ""sleetIntensity"": 0,
                    ""snowIntensity"": 0,
                    ""temperature"": 28,
                    ""temperatureApparent"": 27.95,
                    ""uvHealthConcern"": 2,
                    ""uvIndex"": 6,
                    ""visibility"": 9.99,
                    ""weatherCode"": 1100,
                    ""windDirection"": 353.81,
                    ""windGust"": 8.88,
                    ""windSpeed"": 4.63
                    }
                    },
                    ""location"": {
                    ""lat"": 38.7077522277832,
                    ""lon"": -9.136591911315918,
                    ""name"": ""Lisboa, Portugal"",
                    ""type"": ""administrative""
                    }
                }"
            },
            new object[]
            {
                $"{Guid.NewGuid()}. Weather from the source Weatherbit.io for location {{data.[0].country_code}} {{data.[0].city_name}}. Temperature is {{data.[0].temp}} C (feels like {{data.[0].app_temp}} C). {{data.[0].weather.description}}. Wind speed {{data.[0].wind_spd}} kilometers per hour.",
                @"{
                    ""count"": 1,
                    ""data"": [
                    {
                    ""app_temp"": 27.1,
                    ""aqi"": 42,
                    ""city_name"": ""Lisbon"",
                    ""clouds"": 0,
                    ""country_code"": ""PT"",
                    ""datetime"": ""2023-06-22:14"",
                    ""dewpt"": 13.7,
                    ""dhi"": 116.01,
                    ""dni"": 888.46,
                    ""elev_angle"": 56.39,
                    ""ghi"": 848.29,
                    ""gust"": null,
                    ""h_angle"": 11.2,
                    ""lat"": 38.71667,
                    ""lon"": -9.13333,
                    ""ob_time"": ""2023-06-22 14:36"",
                    ""pod"": ""d"",
                    ""precip"": 0,
                    ""pres"": 1005.8,
                    ""rh"": 44,
                    ""slp"": 1020,
                    ""snow"": 0,
                    ""solar_rad"": 848.3,
                    ""sources"": [
                    ""LPPT""
                    ],
                    ""state_code"": ""14"",
                    ""station"": ""LPPT"",
                    ""sunrise"": ""05:11"",
                    ""sunset"": ""20:05"",
                    ""temp"": 27,
                    ""timezone"": ""Europe/Lisbon"",
                    ""ts"": 1687444572,
                    ""uv"": 7.7769427,
                    ""vis"": 16,
                    ""weather"": {
                    ""code"": 800,
                    ""icon"": ""c01d"",
                    ""description"": ""Clear sky""
                    },
                    ""wind_cdir"": ""NNW"",
                    ""wind_cdir_full"": ""north-northwest"",
                    ""wind_dir"": 330,
                    ""wind_spd"": 5.7
                    }
                    ]
                }"
            },
            new object[]
            {
                $"{Guid.NewGuid()}.Weather from the source WeatherStack for location {{location.country}} {{location.name}}. Temperature is {{current.temperature}} C (feels like {{current.feelslike}} C). {{current.weather_descriptions.[0]}}. Wind speed {{current.wind_speed}} kilometers per hour.",
                @"{
                    ""request"": {
                    ""type"": ""City"",
                    ""query"": ""Lisbon, Portugal"",
                    ""language"": ""en"",
                    ""unit"": ""m""
                    },
                    ""location"": {
                    ""name"": ""Lisbon"",
                    ""country"": ""Portugal"",
                    ""region"": ""Lisboa"",
                    ""lat"": ""38.717"",
                    ""lon"": ""-9.133"",
                    ""timezone_id"": ""Europe/Lisbon"",
                    ""localtime"": ""2023-06-22 16:09"",
                    ""localtime_epoch"": 1687450140,
                    ""utc_offset"": ""1.0""
                    },
                    ""current"": {
                    ""observation_time"": ""03:09 PM"",
                    ""temperature"": 27,
                    ""weather_code"": 116,
                    ""weather_icons"": [
                    ""https://cdn.worldweatheronline.com/images/wsymbols01_png_64/wsymbol_0002_sunny_intervals.png""
                    ],
                    ""weather_descriptions"": [
                    ""Partly cloudy""
                    ],
                    ""wind_speed"": 20,
                    ""wind_degree"": 330,
                    ""wind_dir"": ""NNW"",
                    ""pressure"": 1020,
                    ""precip"": 0,
                    ""humidity"": 45,
                    ""cloudcover"": 25,
                    ""feelslike"": 27,
                    ""uv_index"": 7,
                    ""visibility"": 10,
                    ""is_day"": ""yes""
                    }
                }"
            }
    };


    [Benchmark(Baseline = true)]
    [ArgumentsSource(nameof(BuildTemplateFromJsonData))]
    public string Basic(string template, string json)
    {
        var tokens = _templateService.GetAllPlaceholders(template).Distinct().ToList();

        foreach (var token in tokens)
        {
            template = template.Replace($"{{{{{token}}}}}",
                _jsonParserService.GetValueByPath<object>(json, token).ToString());
        }

        return template;
    }

    [Benchmark]
    [ArgumentsSource(nameof(BuildTemplateFromJsonData))]
    public string WithBrackets(string template, string json)
    {
        var tokens = _templateService.GetAllPlaceholdersWithBrackets(template).Distinct().ToList();

        foreach (var token in tokens)
        {
            template = template.Replace(token,
                _jsonParserService.GetValueByPath<object>(json, token[2..^2]).ToString());
        }

        return template;
    }

    [Benchmark]
    [ArgumentsSource(nameof(BuildTemplateFromJsonData))]
    public string PrebuiltTemplate(string template, string json)
    {
        var tokens = _templateService.GetAllPlaceholdersWithBrackets(template)
            .Distinct()
            .ToDictionary(x => x, x => _jsonParserService.GetValueByPath<object>(json, x[2..^2]).ToString());

        return tokens.Keys.Aggregate(template, (current, token) => current.Replace(token, tokens[token]));
    }

    [Benchmark]
    [ArgumentsSource(nameof(BuildTemplateFromJsonData))]
    public string StringCreate(string template, string json)
    {
        var tokens = _templateService.GetAllPlaceholdersWithBrackets(template)
            .Select(x => (token: x, value: _jsonParserService.GetValueByPath<object>(json, x[2..^2]).ToString()))
            .ToList();

        var length = tokens.Aggregate(template.Length, (i, pair) => i - pair.token.Length + pair.value!.Length);

        return string.Create(length, (tokens, template), (span, tuple) =>
        {
            var templateIndex = 0;
            var spanIndex = 0;
            foreach (var token in tuple.tokens)
            {
                var newTemplateIndex = tuple.template[templateIndex..].IndexOf(token.token, StringComparison.Ordinal) + templateIndex;

                if (templateIndex < newTemplateIndex)
                {
                    var l = newTemplateIndex - templateIndex;
                    tuple.template.AsSpan(templateIndex, l).CopyTo(span[spanIndex..]);
                    spanIndex += l;
                    templateIndex = newTemplateIndex + token.token.Length;
                }

                token.value.AsSpan().CopyTo(span[spanIndex..]);
                spanIndex += token.value!.Length;
            }

            if (templateIndex < template.Length)
            {
                tuple.template.AsSpan(templateIndex, template.Length - templateIndex).CopyTo(span[spanIndex..]);
            }
        });
    }
}