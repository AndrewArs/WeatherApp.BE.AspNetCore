using System.Reflection;
using FluentMigrator;
using FluentMigrator.Builders.Create.Table;
using FluentMigrator.Infrastructure;
using Humanizer;

namespace Infrastructure.Persistence.Migrations.Common;

public abstract class CreateNewTableMigration<T> : Migration
{
    protected virtual string TableName => typeof(T).Name.Underscore().Pluralize(false);
    protected virtual Dictionary<string, Action<ICreateTableWithColumnOrSchemaOrDescriptionSyntax>> CustomMapping => new();

    public override void Up()
    {
        var table = Create.Table(TableName);

        //var nullabilityContext = new NullabilityInfoContext(); // doesn't supported in net5.0 runtime which is used be dotnet-fm tool
        foreach (var propertyInfo in GetProperties())
        {
            if (CustomMapping.TryGetValue(propertyInfo.Name, out var value))
            {
                value(table);
            }
            else
            {
                IFluentSyntax column = table.WithColumn(propertyInfo.Name.Underscore());
                var propertyType = propertyInfo.PropertyType;
                var nullableUnderlyingType = Nullable.GetUnderlyingType(propertyType);
                var isNullable = nullableUnderlyingType != null;
                propertyType = nullableUnderlyingType ?? propertyType;
                switch (Type.GetTypeCode(propertyType))
                {
                    case TypeCode.Boolean:
                        ((ICreateTableColumnAsTypeSyntax)column).AsBoolean();
                        break;
                    case TypeCode.Byte:
                        ((ICreateTableColumnAsTypeSyntax)column).AsByte();
                        break;
                    case TypeCode.DateTime:
                        ((ICreateTableColumnAsTypeSyntax)column).AsDateTime();
                        break;
                    case TypeCode.Decimal:
                        ((ICreateTableColumnAsTypeSyntax)column).AsDecimal();
                        break;
                    case TypeCode.Double:
                        ((ICreateTableColumnAsTypeSyntax)column).AsDouble();
                        break;
                    case TypeCode.Int16:
                        ((ICreateTableColumnAsTypeSyntax)column).AsInt16();
                        break;
                    case TypeCode.Int32:
                        ((ICreateTableColumnAsTypeSyntax)column).AsInt32();
                        break;
                    case TypeCode.Int64:
                        ((ICreateTableColumnAsTypeSyntax)column).AsInt64();
                        break;
                    case TypeCode.String:
                        ((ICreateTableColumnAsTypeSyntax)column).AsString();
                        break;
                    case TypeCode.Object:

                        if (propertyType.Name.Equals(nameof(DateTimeOffset)))
                        {
                            ((ICreateTableColumnAsTypeSyntax)column).AsDateTimeOffset();
                        }
                        else if (propertyType.Name.Equals(nameof(Guid)))
                        {
                            ((ICreateTableColumnAsTypeSyntax)column).AsGuid();
                        }
                        else
                        {
                            goto default;
                        }
                        break;
                    case TypeCode.Empty:
                    case TypeCode.DBNull:
                    case TypeCode.Char:
                    case TypeCode.SByte:
                    case TypeCode.UInt16:
                    case TypeCode.UInt32:
                    case TypeCode.UInt64:
                    case TypeCode.Single:
                    default: throw new ArgumentException($"Unhandled custom type {Enum.GetName(Type.GetTypeCode(propertyType))}. Please add CustomMapping", propertyInfo.PropertyType.Name);
                }

                //if (nullabilityContext.Create(propertyInfo).ReadState == NullabilityState.NotNull)
                //{
                //    ((ICreateTableColumnOptionOrWithColumnSyntax)column).NotNullable();
                //}
                //else
                //{
                //    ((ICreateTableColumnOptionOrWithColumnSyntax)column).Nullable();
                //}
                if (isNullable)
                {
                    ((ICreateTableColumnOptionOrWithColumnSyntax)column).Nullable();
                }
                else
                {
                    ((ICreateTableColumnOptionOrWithColumnSyntax)column).NotNullable();
                }
                
                if (propertyInfo.Name.Equals("id", StringComparison.InvariantCultureIgnoreCase))
                {
                    ((ICreateTableColumnOptionOrWithColumnSyntax)column).PrimaryKey();
                }
            }
        }
    }

    public override void Down()
    {
        Delete.Table(TableName);
    }

    private ICollection<PropertyInfo> GetProperties()
    {
        var properties = typeof(T).GetProperties().ToList();

        var idProp = properties.FirstOrDefault(x => x.Name.Equals("id", StringComparison.InvariantCultureIgnoreCase));
        if (idProp != null)
        {
            properties.Remove(idProp);
            properties.Insert(0, idProp);
        }

        return properties;
    }
}