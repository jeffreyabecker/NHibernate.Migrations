using System;
using System.Linq.Expressions;
using NHibernate.DdlGen.Model;

namespace NHibernate.Migrations.Fluent.Builders
{
    public interface ITableBuilder<TColumns>
    {
        ITableBuilder<TColumns> PrimaryKey(Expression<Func<TColumns, object>> keyExpression, string name = null, bool isIdentity = false);
        ITableBuilder<TColumns> ForeignKey(Expression<Func<TColumns, object>> dependentKeyExpression, DbName references, bool cascade = false, string name = null);
        ITableBuilder<TColumns> Comment(string comment);

    }

    
}