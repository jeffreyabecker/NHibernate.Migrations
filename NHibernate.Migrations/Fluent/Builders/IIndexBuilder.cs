using NHibernate.DdlGen.Model;

namespace NHibernate.Migrations.Fluent.Builders
{
    public interface IIndexBuilder
    {
        IIndexBuilder On(DbName tableName);
        IIndexBuilder Columns(params string[] columnNames);
        IIndexBuilder Unique();
    }
}