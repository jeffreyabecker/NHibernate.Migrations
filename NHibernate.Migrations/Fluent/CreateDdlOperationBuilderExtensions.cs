using System;
using System.Linq;
using NHibernate.DdlGen.Model;
using NHibernate.DdlGen.Operations;
using NHibernate.Migrations.Fluent.Builders;

namespace NHibernate.Migrations.Fluent
{
    public static class CreateDdlOperationBuilderExtensions
    {

        public static IIndexBuilder On(this IIndexBuilder builder, string name)
        {
            return builder.On(new DbName(name));
        }

        public static ITableBuilder<TColumns> Table<TColumns>(this ICreateDdlOperationBuilder builder, DbName name, Func<IColumnBuilder, TColumns> columnBuilder)
        {
            var tableBuilder = new TableBuilder<TColumns>(name, columnBuilder, false);
            builder.AddOperationsFuture(()=>tableBuilder.GetOperations());
            return tableBuilder;
        }

        public static ITableBuilder<TColumns> Table<TColumns>(this ICreateDdlOperationBuilder builder, string name, Func<IColumnBuilder, TColumns> columnBuilder)
        {
            return Table(builder, new DbName(name), columnBuilder);
        }

        public static ITableBuilder<TColumns> TemporaryTable<TColumns>(this ICreateDdlOperationBuilder builder, string name, Func<IColumnBuilder, TColumns> columnBuilder)
        {
            var tableBuilder = new TableBuilder<TColumns>(new DbName(name), columnBuilder, true);
            builder.AddOperationsFuture(() => tableBuilder.GetOperations());
            return tableBuilder;
        }

        public static IIndexBuilder Index(this ICreateDdlOperationBuilder builder, string name = null)
        {
            var indexBuilder = new IndexBuilder(name);
            builder.AddOperationsFuture(()=>indexBuilder.GetOperations());
            return indexBuilder;
        }

        public static void Sequence(this ICreateDdlOperationBuilder builder, string seqenceName, int initialValue = 1,
                                    int increment = 1)
        {
            Sequence(builder, new DbName(seqenceName), initialValue, increment);
        }
        public static void Sequence(this ICreateDdlOperationBuilder builder, DbName seqenceName, int initialValue = 1, int increment = 1)
        {
            builder.AddOperationsFuture(()=> new []{new CreateSequenceDdlOperation(new CreateSequenceModel
            {
                Name = seqenceName,
                IncrementSize = increment,
                InitialValue = initialValue
            })});   
        }

    }
}