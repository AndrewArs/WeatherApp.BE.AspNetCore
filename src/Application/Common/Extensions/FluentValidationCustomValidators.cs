using FluentValidation;

namespace Application.Common.Extensions;

public static class FluentValidationCustomValidators
{
    public static IRuleBuilderOptionsConditions<T, string> Url<T>(this IRuleBuilder<T, string> ruleBuilder)
    {
        return ruleBuilder.Custom((url, context) =>
        {
            if (!url.StartsWith("http") && !url.StartsWith("https"))
            {
                context.AddFailure("Url should start with protocol http/https");
            }

            if (!Uri.TryCreate(url, UriKind.Absolute, out _))
            {
                context.AddFailure("Invalid Url");
            }
        });
    }
}