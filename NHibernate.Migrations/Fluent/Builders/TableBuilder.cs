using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using NHibernate.DdlGen.Model;
using NHibernate.DdlGen.Operations;

namespace NHibernate.Migrations.Fluent.Builders
{
    public class TableBuilder<TColumns> : ITableBuilder<TColumns>
    {
        private readonly DbName _name;
        private readonly List<FluentColumnModel> _columns;
        private readonly TColumns _columnModel;
        private PkInfo _pk;
        private readonly List<FkInfo>  _foreginKeys = new List<FkInfo>();
        private string _comment;
        private readonly bool _temporary;
        private class MyColumnBuilder : IColumnBuilder{}

        private class PkInfo
        {
            public string Name { get; set; }
            public List<FluentColumnModel> Columns { get; set; }
            public bool IsIdentity { get; set; }

        }


        private class FkInfo
        {
            public string Name { get; set; }
            public List<FluentColumnModel> Columns { get; set; }
            public DbName ReferencedTableName { get; set; }
            public bool CascadeDelete { get; set; }
        }
   


        public TableBuilder(DbName name, Func<IColumnBuilder, TColumns> columnBuilder, bool temporary)
        {
            _name = name;
            _columnModel = columnBuilder(new MyColumnBuilder());
            _columns = GetColumns(_columnModel);
            _temporary = temporary;
        }


        private static List<FluentColumnModel> GetColumns(object model)
        {
            var result = new List<FluentColumnModel>();
            var fluentModel = model as FluentColumnModel;
            if (fluentModel != null)
            {
                result.Add(fluentModel);
                return result;
            }
            var cols = model.GetType().GetProperties()
                            .Where(p => p.CanRead && typeof (FluentColumnModel).IsAssignableFrom(p.PropertyType))
                            .Select(p => new
                            {
                                p.Name,
                                Col = (FluentColumnModel) p.GetValue(model, null)
                            });
            
            foreach (var c in cols)
            {
                c.Col.Name = c.Name;
                result.Add(c.Col);
            }
            return result;
        }




        public ITableBuilder<TColumns> PrimaryKey(Expression<Func<TColumns, object>> keyExpression, string name = null, bool isIdentity = false)
        {
            if(_pk != null)
                throw new HibernateException("Cant two PK's to the same table");
            var keyModel = keyExpression.Compile()(_columnModel);
            var keyColumns = GetColumns(keyModel);
            _pk = new PkInfo
            {
                Columns = keyColumns,
                Name = name,
                IsIdentity = isIdentity
            };
            
            return this;
        }

        public ITableBuilder<TColumns> Comment(string comment)
        {
            _comment = comment;
            return this;
        }

        public ITableBuilder<TColumns> ForeignKey(Expression<Func<TColumns, object>> dependentKeyExpression, DbName references, bool cascade = false, string name = null)
        {
            var fkModel = dependentKeyExpression.Compile()(_columnModel);
            _foreginKeys.Add(new FkInfo
            {
                ReferencedTableName = references,
                Columns = GetColumns(fkModel),
                Name = name, 
                CascadeDelete = cascade
            });
            return this;
        }

        public IEnumerable<IDdlOperation> GetOperations()
        {
            if (_temporary)
            {
                return new[] {new CreateTemporaryTableDdlOperation(CreateTemporaryTableModel())};
            }
            var result = new List<IDdlOperation>();
            var createTableModel = GetCreateTableModel();
            result.Add(new CreateTableDdlOperation(createTableModel));
            result.AddRange(createTableModel.ForeignKeys.Select(fk => new CreateForeignKeyOperation(fk)));
            return result;
        }

        private CreateTemporaryTableModel CreateTemporaryTableModel()
        {
            var model = new CreateTemporaryTableModel
            {
                Name = (_name),
                Columns = _columns.Select(c => new ColumnModel
                {
                    CheckConstraint = null,
                    Comment = null,
                    DefaultValue = c.DefaultValue,
                    SqlType = c.SqlType,
                    SqlTypeCode = c.SqlTypeCode,
                    Name = c.Name,
                    Nullable = c.Nullable
                }).ToList()
            };
            return model;
        }

        private CreateTableModel GetCreateTableModel()
        {
            
            var columns = _columns.ToDictionary(c => c, c => new ColumnModel
            {
                CheckConstraint = null,
                Comment = null,
                DefaultValue = c.DefaultValue,
                SqlType = c.SqlType,
                SqlTypeCode = c.SqlTypeCode,
                Name = c.Name,
                Nullable = c.Nullable
            });
            var pkColumns = columns.Where(p => _pk.Columns.Contains(p.Key)).Select(c => c.Value).ToList();
            var pk = new PrimaryKeyModel
            {
                Columns = pkColumns,
                Name = _pk.Name,
                Identity = _pk.IsIdentity,
            };
            var dependentTable = (_name);
            var fks = _foreginKeys.Select(fk => new ForeignKeyModel
            {
                Name = fk.Name,
                DependentTable = dependentTable,
                ForeignKeyColumns = columns.Where(c => fk.Columns.Contains(c.Key)).Select(c => c.Value.Name).ToList(),
                CascadeDelete = fk.CascadeDelete,
                IsReferenceToPrimaryKey = true,
                ReferencedTable = (fk.ReferencedTableName)
            }).ToList();

            var model = new CreateTableModel
            {
                Checks = new TableCheckModel[0],
                Name = dependentTable,
                UniqueIndexes = new IndexModel[0],
                Comment = _comment,
                Columns = columns.Select(c => c.Value).ToList(),
                PrimaryKey = pk,
                ForeignKeys = fks
            };
            return model;
        }
    }
}