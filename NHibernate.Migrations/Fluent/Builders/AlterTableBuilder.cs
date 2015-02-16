using System.Collections.Generic;
using System.Linq;
using Antlr.Runtime.Misc;
using NHibernate.DdlGen.Model;
using NHibernate.DdlGen.Operations;

namespace NHibernate.Migrations.Fluent.Builders
{
    public class AlterTableBuilder : IAlterTableBuilder
    {




        private readonly List<Func<IDdlOperation>> _operations = new List<Func<IDdlOperation>>();
        public AlterTableBuilder(DbName tableName)
        {
            TableName = tableName;
        }

        public IEnumerable<IDdlOperation> GetOperations()
        {
            return _operations.Select(x=>x());
        }


        public DbName TableName { get; set; }

        public void AddOperation(IDdlOperation operation)
        {
            _operations.Add(()=>operation);
        }

        public void AddOperation(Func<IDdlOperation> promise)
        {
            _operations.Add(promise);
        }
    }
}