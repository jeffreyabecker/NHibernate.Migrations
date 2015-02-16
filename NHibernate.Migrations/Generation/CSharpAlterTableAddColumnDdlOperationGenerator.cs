using System;
using System.Data;
using System.Linq;
using System.Text;
using NHibernate.DdlGen.Operations;

namespace NHibernate.Migrations.Generation
{
    public class CSharpAlterTableAddColumnDdlOperationGenerator : DdlGeneratorBase<AlterTableAddColumnDdlOperation>
    {
        private static readonly DbType[] _numericTypes =
        {
            DbType.Guid, DbType.Boolean, DbType.Byte, DbType.Currency, DbType.Date, DbType.DateTime, DbType.DateTime2,
            DbType.DateTimeOffset, DbType.Decimal, DbType.Double, DbType.Int16, DbType.Int32, DbType.Int64, DbType.SByte,
            DbType.Single, DbType.Time, DbType.UInt16, DbType.UInt32, DbType.UInt64
        };

        private static readonly DbType[] _stringTypes = { DbType.AnsiString, DbType.Binary, DbType.String };

        
        protected override void AppendOperation(StringBuilder sb, int index, AlterTableAddColumnDdlOperation op)
        {
            throw new NotImplementedException();
            //var model = op.Model;
            //sb.AppendFormat("            alter.Table(\"{0}\")", model.Table).AppendLine();
            //sb.AppendFormat("            .AddColumn(\"{0}\", cb=>cb.", model.Column.Name);
            //if (model.Column.SqlTypeCode != null)
            //{
            //    sb.Append(model.Column.SqlTypeCode.DbType);
            //    if (_stringTypes.Contains(model.Column.SqlTypeCode.DbType))
            //    {
            //        sb.Append()
            //    }
            //}


            //sb.AppendLine();
        }
    }
}