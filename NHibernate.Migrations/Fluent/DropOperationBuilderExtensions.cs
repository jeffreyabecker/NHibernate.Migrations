using NHibernate.DdlGen.Model;
using NHibernate.DdlGen.Operations;
using NHibernate.Migrations.Fluent.Builders;

namespace NHibernate.Migrations.Fluent
{
    public static class DropOperationBuilderExtensions
    {
        public static void Table(this IDropDdlOperationBuilder builder, DbName name)
        {
            var threePart = name;
            var operation = new DropTableDdlOperation(threePart);
            builder.AddOperationsFuture(()=>new[]{operation});
        }

        public static void Table(this IDropDdlOperationBuilder builder, string name)
        {
            Table(builder, new DbName(name));
        }

        public static void Index(this IDropDdlOperationBuilder builder, string name, DbName tableName)
        {
            var model = new IndexModel
            {
                Clustered = false,
                Name = name,
                TableName = tableName
            };
            builder.AddOperationsFuture(()=>new[]{new DropIndexDdlOperation(model, true)});
        }

        public static void Index(this IDropDdlOperationBuilder builder, string name, string tableName)
        {
            Index(builder, name, new DbName(tableName));
        }

        public static void Sequence(this IDropDdlOperationBuilder builder, string sequenceName)
        {
            builder.AddOperationsFuture(()=>new[]{new DropSequenceDdlOperation(sequenceName)});
        }
    }
}
