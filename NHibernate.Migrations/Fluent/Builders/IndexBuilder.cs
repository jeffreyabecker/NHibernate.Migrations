using System.Collections.Generic;
using NHibernate.DdlGen.Model;
using NHibernate.DdlGen.Operations;

namespace NHibernate.Migrations.Fluent.Builders
{
    public class IndexBuilder : IIndexBuilder
    {
        private readonly string _name;
        private DbName _tableName;
        private string[] _columnNames;
        private bool _unique = false;
        private bool _clustered = false;

        public IndexBuilder(string name)
        {
            _name = name;
        }

        public IIndexBuilder On(DbName tableName)
        {
            _tableName = tableName;
            return this;
        }

        public IIndexBuilder Columns(params string[] columnNames)
        {
            _columnNames = columnNames;
            return this;
        }

        public IIndexBuilder Unique()
        {
            _unique = true;
            return this;
        }

        public IIndexBuilder Clustered()
        {
            _clustered = true;
            return this;
        }

        public IEnumerable<IDdlOperation> GetOperations()
        {
            var indexModel = new IndexModel
            {
                TableName = (_tableName),
                Columns = _columnNames,
                Clustered = _clustered,
                Name = _name,
                Unique = _unique
            };
            return new[] {new CreateIndexDdlOperation(indexModel)};

        }
    }
}