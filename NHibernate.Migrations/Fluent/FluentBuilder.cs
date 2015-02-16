using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate.DdlGen.Operations;
using NHibernate.Migrations.Fluent.Builders;

namespace NHibernate.Migrations.Fluent
{
    public class FluentBuilder: IDdlOperationBuilderSurface, ICreateDdlOperationBuilder, IAlterDdlOperationBuilder, IDropDdlOperationBuilder
    {
        public ICreateDdlOperationBuilder Create { get { return this; } }
        public IAlterDdlOperationBuilder Alter { get { return this; } }
        public IDropDdlOperationBuilder Drop { get { return this; } }
        public void Run(IDdlOperation operation)
        {
            _futures.Add(() => new[] { operation });
        }
        private readonly List<Func<IEnumerable<IDdlOperation>>> _futures = new List<Func<IEnumerable<IDdlOperation>>>();
        public void AddOperationsFuture(Func<IEnumerable<IDdlOperation>> future)
        {
            _futures.Add(future);
        }

        public IEnumerable<IDdlOperation> GeneratedOperations
        {
            get { return _futures.Select(f => f()).SelectMany(o=>o); }
        }

    }
}
