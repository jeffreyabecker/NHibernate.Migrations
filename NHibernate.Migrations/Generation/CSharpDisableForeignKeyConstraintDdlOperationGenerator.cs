using System.Text;
using NHibernate.DdlGen.Operations;

namespace NHibernate.Migrations.Generation
{
    public class CSharpDisableForeignKeyConstraintDdlOperationGenerator : DdlGeneratorBase<DisableForeignKeyConstraintDdlOperation>
    {
        protected override void AppendOperation(StringBuilder sb, int index, DisableForeignKeyConstraintDdlOperation op)
        {
            sb.AppendLine("            builder.Run(new DisableForeignKeyConstraintDdlOperation());");
        }
    }


}