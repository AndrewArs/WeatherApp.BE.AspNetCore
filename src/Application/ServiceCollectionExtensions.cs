using System.Reflection;
using Application.Common.Behaviors;
using Application.Mediatr.ForecastProvider.Commands.Create;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Application;
public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(PerformanceBehavior<,>));
            cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
        });

        services.AddValidationBehavior();

        return services;
    }

    private static void AddValidationBehavior(this IServiceCollection services)
    {
        var validatableTypes = GetValidatableTypes();
        var pipelineBehaviorType = typeof(IPipelineBehavior<,>);
        var validationBehaviorType = typeof(ValidationBehavior<,>);
        foreach (var validatableType in validatableTypes)
        {
            var resultType = validatableType
                .GetInterfaces()
                .First(x => x.GetGenericTypeDefinition() == typeof(IRequest<>))
                .GetGenericArguments()
                .First();
            var successType = resultType.GetGenericArguments().First();

            services.AddTransient(pipelineBehaviorType.MakeGenericType(validatableType, resultType),
                validationBehaviorType.MakeGenericType(validatableType, successType));
        }
    }

    private static Type[] GetValidatableTypes()
    {
        return typeof(AddForecastProviderCommand).Assembly
            .GetTypes()
            .Where(type => type.BaseType?.IsGenericType == true)
            .Where(type => type.BaseType!.GetGenericTypeDefinition() == typeof(AbstractValidator<>))
            .Select(type => type.BaseType!.GetGenericArguments().First())
            .ToArray();
    }
}
