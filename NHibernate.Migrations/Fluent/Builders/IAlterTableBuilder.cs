using System.Security.Cryptography.X509Certificates;
using Antlr.Runtime.Misc;
using NHibernate.DdlGen.Model;
using NHibernate.DdlGen.Operations;

namespace NHibernate.Migrations.Fluent.Builders
{
    public interface IAlterTableBuilder
    {
        DbName TableName { get; }
    
        void AddOperation(Func<IDdlOperation> promise);
    }

    public interface IForeignKeyBuilder
    {
        IForeignKeyBuilder Named(string name);
        IForeignKeyBuilder References(DbName tableName, params string[] columns);

    }
}
