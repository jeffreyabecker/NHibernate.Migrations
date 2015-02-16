using System.Text;
using NHibernate.DdlGen.Operations;

namespace NHibernate.Migrations.Generation
{
    public class CSharpDropTableDdlOperationGenerator : DdlGeneratorBase<DropTableDdlOperation>
    {
        protected override void AppendOperation(StringBuilder sb, int index, DropTableDdlOperation op)
        {
            var operation = op;
            sb.AppendFormat("            builder.Drop.Table(\"{0}\");", operation.TableName);
            sb.AppendLine();
        }
    }
}