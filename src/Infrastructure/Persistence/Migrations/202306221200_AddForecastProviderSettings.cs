using Domain.Entities;
using FluentMigrator;
using FluentMigrator.Builders.Create.Table;
using Humanizer;
using Infrastructure.Persistence.Migrations.Common;

namespace Infrastructure.Persistence.Migrations;

[Migration(202306221200)]
public class AddForecastProviderSettings : CreateNewTableMigration<ForecastProviderSettings>
{
    protected override Dictionary<string, Action<ICreateTableWithColumnOrSchemaOrDescriptionSyntax>> CustomMapping => new()
    {
        {
            nameof(ForecastProviderSettings.Name), syntax =>
            {
                syntax.WithColumn(nameof(ForecastProviderSettings.Name).Underscore())
                    .AsString()
                    .Unique();
            }
        }
    };
}