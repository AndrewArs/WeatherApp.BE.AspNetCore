using Application.Common.Extensions;
using FluentValidation;

namespace Application.Mediatr.ForecastProvider.Commands.Update;

public class UpdateForecastProviderCommandValidator : AbstractValidator<UpdateForecastProviderCommand>
{
    public UpdateForecastProviderCommandValidator(IJsonParserService jsonParserService, ITemplateService templateService)
    {
        RuleFor(x => x.Url)!
            .NotEmpty()!
            .Url()
            .When(x => x.Url is not null);

        RuleFor(x => x.ForecastTemplatePath)!
            .NotEmpty()!
            .Must(x => templateService.GetAllPlaceholders(x!).All(jsonParserService.IsValidPath))
            .When(x => x.ForecastTemplatePath is not null);
        
        RuleFor(x => x.TemperaturePath)!
            .NotEmpty()!
            .Must(jsonParserService.IsValidPath!)
            .When(x => x.TemperaturePath is not null);

        RuleFor(x => x.Name)
            .NotEmpty()
            .When(x => x.Name is not null);
    }
}