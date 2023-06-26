using Application.Common.Extensions;
using FluentValidation;

namespace Application.Mediatr.ForecastProvider.Commands.Create;

public class AddForecastProviderValidator : AbstractValidator<AddForecastProviderCommand>
{
    public AddForecastProviderValidator(IJsonParserService jsonParserService, ITemplateService templateService)
    {
        RuleFor(x => x.ForecastTemplatePath)
            .Must(x => templateService.GetAllPlaceholders(x).All(jsonParserService.IsValidPath));

        RuleFor(x => x.TemperaturePath)
            .Must(jsonParserService.IsValidPath);

        RuleFor(x => x.Url)
            .NotEmpty()
            .Url();

        RuleFor(x => x.Name)
            .NotEmpty();
    }
}