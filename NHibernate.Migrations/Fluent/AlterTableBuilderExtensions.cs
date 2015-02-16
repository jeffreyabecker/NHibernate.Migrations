using Antlr.Runtime.Misc;
using NHibernate.DdlGen.Model;
using NHibernate.DdlGen.Operations;
using NHibernate.Migrations.Fluent.Builders;

namespace NHibernate.Migrations.Fluent
{
    public static  class AlterTableBuilderExtensions
    {
        private class MyColumnBuilder : IColumnBuilder { }


        public static void RenameColumn(this IAlterTableBuilder builder, string from, string to)
        {
            builder.AddOperation(() => new AlterTableRenameColumnOperation(new RenameColumnModel
            {
                Table = builder.TableName,
                NewColumnName = to,
                OldColumnName = from
            }));
        }

        public static void RenameTable(this IAlterTableBuilder builder, string newName)
        {
            builder.AddOperation(() => new AlterTableRenameTableOperation(new RenameTableModel
            {
                OldTableName = builder.TableName,
                NewTableName = new DbName(newName)
            }));
        }


        public static void AddColumn(this IAlterTableBuilder builder, string columnName, Func<IColumnBuilder, FluentColumnModel> cb)
        {
            var fluentColumn = cb(new MyColumnBuilder());
            var model = new AddOrAlterColumnModel
            {
                Table = builder.TableName,
                Column = new ColumnModel
                {
                    CheckConstraint = null,
                    Comment = null,
                    DefaultValue = fluentColumn.DefaultValue,
                    SqlType = fluentColumn.SqlType,
                    SqlTypeCode = fluentColumn.SqlTypeCode,
                    Name = columnName,
                    Nullable = fluentColumn.Nullable
                }
            };
            var op = new AlterTableAddColumnDdlOperation(model);
            builder.AddOperation(() => op);
        }

        public static void DropColumn(this IAlterTableBuilder builder, string columnName)
        {
            builder.AddOperation(() => new AlterTableDropColumnDdlOperation(new DropColumnModel { Table = builder.TableName, Column = columnName }));
        }

        public static void AlterColumn(this IAlterTableBuilder builder, string columnName, Func<IColumnBuilder, FluentColumnModel> cb)
        {
            var fluentColumn = cb(new MyColumnBuilder());
            var model = new AddOrAlterColumnModel
            {
                Table = builder.TableName,
                Column = new ColumnModel
                {
                    CheckConstraint = null,
                    Comment = null,
                    DefaultValue = fluentColumn.DefaultValue,
                    SqlType = fluentColumn.SqlType,
                    SqlTypeCode = fluentColumn.SqlTypeCode,
                    Name = columnName,
                    Nullable = fluentColumn.Nullable
                }
            };
            builder.AddOperation(() => new AlterTableAlterColumnDdlOperation(model));
        }

        public static void DropForeignKey(this IAlterTableBuilder builder, string fkName)
        {
            builder.AddOperation(() => new DropForeignKeyDdlOperation(new ForeignKeyModel
            {
                Name = fkName,
                DependentTable = builder.TableName
            }));
        }

        public static void DropIndex(this IAlterTableBuilder builder, string indexName, bool ifExists = false, bool unique = false)
        {
            builder.AddOperation(() => new DropIndexDdlOperation(new IndexModel
            {
                TableName = builder.TableName,
                Name = indexName,
                Unique = unique
            }, ifExists));
        }

        public static void AddComment(this IAlterTableBuilder builder, string comment)
        {
            builder.AddOperation(()=>new AddTableCommentsDdlOperation(new TableCommentsModel
            {
                TableName = builder.TableName,
                Comment = comment
            }));
        }

        public static IForeignKeyBuilder AddForeignKey(this IAlterTableBuilder builder, params string[] columns )
        {
            var fkb = new ForeignKeyBuilder(builder.TableName, columns);
            builder.AddOperation(fkb.GetOperation);
            return fkb;
        }

        
    }

    public class ForeignKeyBuilder : IForeignKeyBuilder
    {
        private readonly ForeignKeyModel _foreignKeyModel;

        public ForeignKeyBuilder(DbName table, string[] columns)
        {
            _foreignKeyModel = new ForeignKeyModel
            {
                DependentTable = table,
                ForeignKeyColumns = columns,
                
            };
        }

        public IForeignKeyBuilder Named(string name)
        {
            _foreignKeyModel.Name = name;
            return this;
        }

        public IDdlOperation GetOperation()
        {
            return new CreateForeignKeyOperation(_foreignKeyModel);
        }

        public IForeignKeyBuilder References(DbName tableName, params string[] columns)
        {
            _foreignKeyModel.ReferencedTable = tableName;
            _foreignKeyModel.IsReferenceToPrimaryKey = columns != null;
            _foreignKeyModel.PrimaryKeyColumns = columns ?? new string[0];
            return this;
        }

        IForeignKeyBuilder OnDeleteCascade()
        {
            _foreignKeyModel.CascadeDelete = true;
            return this;
        }
    }
}