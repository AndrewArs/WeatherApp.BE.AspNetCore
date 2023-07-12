using Domain.Entities;
using FluentMigrator;
using Humanizer;
using Infrastructure.Persistence.Migrations.Common;

namespace Infrastructure.Persistence.Migrations;

[Migration(202306221200)]
public class AddForecastProviderSettings : Migration
{
    private readonly string TableName = nameof(ForecastProviderSettings).Underscore();

    public override void Up()
    {
        Create.Table(TableName)
            .WithBaseIdentityEntityColumns()
            .WithColumn(nameof(ForecastProviderSettings.Name).Underscore()).AsFixedLengthString(150).Unique().NotNullable()
            .WithColumn(nameof(ForecastProviderSettings.Url).Underscore()).AsString().NotNullable()
            .WithColumn(nameof(ForecastProviderSettings.TemperaturePath).Underscore()).AsFixedLengthString(100).NotNullable()
            .WithColumn(nameof(ForecastProviderSettings.ForecastTemplatePath).Underscore()).AsString().NotNullable()
            .WithColumn(nameof(ForecastProviderSettings.KeyQueryParamName).Underscore()).AsFixedLengthString(100).NotNullable()
            .WithBaseAuditableEntityColumns();
    }

    public override void Down()
    {
        Delete.Table(TableName);
    }
}