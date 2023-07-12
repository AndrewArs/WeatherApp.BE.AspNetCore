using Domain.Common;
using FluentMigrator.Builders.Create.Table;
using Humanizer;

namespace Infrastructure.Persistence.Migrations.Common;
internal static class FluentMigratorExtensions
{
    public static ICreateTableColumnOptionOrWithColumnSyntax WithBaseIdentityEntityColumns(
        this ICreateTableWithColumnOrSchemaSyntax syntax)
    {
        return syntax.WithColumn(nameof(BaseIdentityEntity.Id).Underscore()).AsGuid().PrimaryKey();
    }

    public static ICreateTableColumnOptionOrWithColumnSyntax WithBaseAuditableEntityColumns(
        this ICreateTableColumnOptionOrWithColumnSyntax syntax)
    {
        return syntax
            .WithColumn(nameof(BaseAuditableEntity.CreatedAt).Underscore()).AsDateTimeOffset().NotNullable()
            .WithColumn(nameof(BaseAuditableEntity.UpdatedAt).Underscore()).AsDateTimeOffset().NotNullable();
    }
}
