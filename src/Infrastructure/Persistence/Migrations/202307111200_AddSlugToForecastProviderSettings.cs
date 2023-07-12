using FluentMigrator;

namespace Infrastructure.Persistence.Migrations;

[Migration(202307111200)]
public class AddSlugToForecastProviderSettings : Migration
{
    private const string TableName = "forecast_provider_settings";
    private const string ColumnName = "slug";

    public override void Up()
    {
        Create.Column(ColumnName)
            .OnTable(TableName)
            .AsString(150)
            .Nullable();

        Execute.Sql(@$"
            create extension if not exists unaccent;
            update {TableName} set ""{ColumnName}""=trim(both '-' from lower(regexp_replace(unaccent(name), '\W','-\1', 'g')));
        ");

        Alter
            .Column(ColumnName)
            .OnTable(TableName)
            .AsString(150)
            .NotNullable()
            .Unique();
    }

    public override void Down()
    {
        Delete.Column(ColumnName)
            .FromTable(TableName);
    }
}