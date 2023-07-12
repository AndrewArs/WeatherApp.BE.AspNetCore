using Application.Mediatr.ForecastProvider.Commands.Create;
using Application.Mediatr.ForecastProvider.Commands.Update;
using Application.Mediatr.ForecastProvider.Shared;

namespace WebApi.Mappings;

public static class WeatherProviderMappings
{
    public static ListOfDto<ForecastProviderDto> ToDto(this ListOf<ForecastProviderResponse> domain)
        => new(domain.Data.Select(x => x.ToDto()).ToList());

    public static ForecastProviderDto ToDto(this ForecastProviderResponse domain)
        => new(domain.Id,
                domain.CreatedAt,
                domain.UpdatedAt,
                domain.Name,
                domain.Slug,
                domain.Url,
                domain.TemperaturePath,
                domain.ForecastTemplatePath,
                domain.KeyQueryParamName);

    public static AddForecastProviderCommand ToDomain(this CreateForecastProviderDto dto)
        => new()
        {
            ForecastTemplatePath = dto.ForecastTemplatePath,
            Name = dto.Name,
            TemperaturePath = dto.TemperaturePath,
            Url = dto.Url,
            KeyQueryParamName = dto.KeyQueryParamName
        };

    public static UpdateForecastProviderCommand ToDomain(this UpdateForecastProviderDto dto, Guid id)
        => new()
        {
            Id = id,
            ForecastTemplatePath = dto.ForecastTemplatePath,
            Name = dto.Name,
            TemperaturePath = dto.TemperaturePath,
            Url = dto.Url,
            KeyQueryParamName = dto.KeyQueryParamName
        };
}