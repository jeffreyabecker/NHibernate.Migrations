using System;
using System.Collections.Generic;
using NHibernate.DdlGen.Operations;

namespace NHibernate.Migrations.Fluent.Builders
{
    public interface IDdlOperationBuilder
    {
        void AddOperationsFuture(Func<IEnumerable<IDdlOperation>> future);
    }

    public interface ICreateDdlOperationBuilder : IDdlOperationBuilder
    {
    }

    public interface IDropDdlOperationBuilder : IDdlOperationBuilder
    {
        
    }

    public interface IAlterDdlOperationBuilder : IDdlOperationBuilder
    {
    }
}