using NHibernate.DdlGen.Model;
using NHibernate.Migrations.Fluent.Builders;

namespace NHibernate.Migrations.Fluent
{
    public static class AlterDdlOperationBuilderExtensions
    {
        public static IAlterTableBuilder Table(this IAlterDdlOperationBuilder builder, DbName name)
        {
            var alter = new AlterTableBuilder(name);
            builder.AddOperationsFuture(()=>alter.GetOperations());
            return alter;
        }

        public static IAlterTableBuilder Table(this IAlterDdlOperationBuilder builder, string name)
        {
            var alter = new AlterTableBuilder(new DbName(name));
            builder.AddOperationsFuture(() => alter.GetOperations());
            return alter;
        }
    }
}