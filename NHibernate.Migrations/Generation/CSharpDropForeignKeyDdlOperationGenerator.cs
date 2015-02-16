using System.Text;
using NHibernate.DdlGen.Operations;

namespace NHibernate.Migrations.Generation
{
    public class CSharpDropForeignKeyDdlOperationGenerator : DdlGeneratorBase<DropForeignKeyDdlOperation>
    {
        protected override void AppendOperation(StringBuilder sb, int index, DropForeignKeyDdlOperation op)
        {
            sb.AppendFormat("            builder.Alter.Table(\"{0}\").DropForeignKey(\"{1}\";", op.Model.DependentTable, op.Model.Name);
            sb.AppendLine();
        }
    }
}